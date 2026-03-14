import { Component, computed, effect, inject, input } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Evolution } from '../../models/api/pokemon';
import { ResourceTextComponent } from '../resource-text/resource-text';
import { TypeBadgeComponent } from '../type-badge/type-badge';
import { EvolutionLink } from './evolution-link';
import { EvolutionNode } from './evolution-node';

@Component({
    selector: 'pokeoneweb-evolution-chart',
    imports: [TypeBadgeComponent, ResourceTextComponent, RouterLink],
    templateUrl: './evolution-chart.html',
    styleUrl: './evolution-chart.scss',
})
export class EvolutionChartComponent {
    evolutions = input.required<Evolution[]>();

    router = inject(Router);

    baseSize = 100;
    horizontalSpacing: number = this.baseSize * 2;
    verticalSpacing: number = this.baseSize * 0.5;
    arrowLength = 10;
    arrowWidth = 6;
    leftPad = 50;
    rightPad = 50;
    topPad = 5;
    bottomPad = 20;

    stages = computed(() => {
        const baseStages = this.evolutions().map((e) => e.baseStage);
        const evolvedStages = this.evolutions().map((e) => e.evolvedStage);

        const distinctStages = [...new Set(baseStages.concat(evolvedStages))];
        return distinctStages;
    });

    maxStageSize = computed(() => {
        let maxCount = 0;

        this.stages().forEach((stage: number) => {
            const evolvedVarietiesInThisStage = this.evolutions()
                .filter((e) => e.evolvedStage === stage)
                .map((e) => e.evolvedResourceName);
            const baseVarietiesInThisStage = this.evolutions()
                .filter((e) => e.baseStage === stage)
                .map((e) => e.baseResourceName);

            const distinctStageVarieties = [
                ...new Set([...evolvedVarietiesInThisStage, ...baseVarietiesInThisStage]),
            ];
            const count = distinctStageVarieties.length;
            if (count > maxCount) {
                maxCount = count;
            }
        });
        console.log(maxCount);

        return maxCount;
    });

    addPaddingForSameStageEvolutions = effect(() => {
        const sameStageEvolutions = this.evolutions().filter((e) => e.baseStage === e.evolvedStage);
        if (sameStageEvolutions.length > 0) {
            if (sameStageEvolutions[0].baseStage === 0) {
                this.leftPad += 2 * this.baseSize;
            } else {
                this.rightPad += 2 * this.baseSize;
            }
        }
    });

    evolutionNodes = computed(() => {
        const basePokemon = this.evolutions().map(
            (e) =>
                <EvolutionNode>{
                    name: e.baseName,
                    resourceName: e.baseResourceName,
                    spriteName: e.baseSpriteName,
                    type1: e.basePrimaryElementalType,
                    type2: e.baseSecondaryElementalType,
                    stage: e.baseStage,
                    sortIndex: e.baseSortIndex,
                    cx: 0,
                    cy: 0,
                    r: this.baseSize / 2,
                },
        );

        const evolvedPokemon = this.evolutions().map(
            (e) =>
                <EvolutionNode>{
                    name: e.evolvedName,
                    resourceName: e.evolvedResourceName,
                    spriteName: e.evolvedSpriteName,
                    type1: e.evolvedPrimaryElementalType,
                    type2: e.evolvedSecondaryElementalType,
                    stage: e.evolvedStage,
                    sortIndex: e.evolvedSortIndex,
                    cx: 0,
                    cy: 0,
                    r: this.baseSize / 2,
                },
        );

        const allPokemon = basePokemon.concat(evolvedPokemon);

        const distinctPokemon: EvolutionNode[] = [];

        allPokemon.forEach((p) => {
            if (!distinctPokemon.map((dp) => dp.resourceName).includes(p.resourceName)) {
                distinctPokemon.push(p);
            }
        });

        let evolutionNodes = distinctPokemon.sort((p1, p2) => {
            if (p1.stage !== p2.stage) {
                return p1.stage - p2.stage;
            } else {
                return p1.sortIndex - p2.sortIndex;
            }
        });

        return this.placeEvolutionNodes(evolutionNodes);
    });

    evolutionLinks = computed(() => {
        return this.evolutions().map((e) => {
            //Same stage evolutions are different
            if (e.baseStage === e.evolvedStage) {
                return this.createSameStageEvolutionLink(e);
            } else {
                return this.createRegularEvolutionLink(e);
            }
        });
    });

    totalWidth = computed(
        () =>
            this.stages().length * this.baseSize +
            (this.stages().length - 1) * this.horizontalSpacing +
            this.leftPad +
            this.rightPad,
    );

    totalHeight = computed(
        () =>
            this.maxStageSize() * (this.baseSize + this.verticalSpacing) +
            this.topPad +
            this.bottomPad,
    );

    navigate(url: string) {
        this.router.navigateByUrl(url);
    }

    private placeEvolutionNodes(evolutionNodes: EvolutionNode[]): EvolutionNode[] {
        const maxStage = Math.max(...this.stages());
        for (let stage = 0; stage <= maxStage; stage++) {
            const nodes = evolutionNodes
                .filter((n) => n.stage === stage)
                .sort((n1, n2) => n1.sortIndex - n2.sortIndex);

            const stageHeight = nodes.length * (this.baseSize + this.verticalSpacing);
            const y0 = this.topPad + (this.totalHeight() - stageHeight) / 2 + this.baseSize / 2;
            const x0 = this.leftPad + this.baseSize / 2;

            for (let i = 0; i < nodes.length; i++) {
                const node = nodes[i];
                node.cy = y0 + i * (this.baseSize + this.verticalSpacing);
                node.cx = x0 + stage * (this.baseSize + this.horizontalSpacing);

                //If the current node only has one incoming evolution, and the previous node
                //by evolution has only one outgoing evolution, make sure that the two nodes are on the
                //same height.
                const incomingEvolutions = this.evolutions()
                    .filter(
                        (e) =>
                            e.evolvedStage === node.stage &&
                            e.evolvedResourceName === node.resourceName,
                    )
                    .filter((e) => e.baseStage !== e.evolvedStage); //Exclude same stage evolutions

                if (incomingEvolutions.length !== 1) {
                    continue;
                }

                const incomingEvolution = incomingEvolutions[0];
                const outgoingEvolutionsOfPreviousNode = this.evolutions().filter(
                    (e) =>
                        e.baseStage === incomingEvolution.baseStage &&
                        e.baseResourceName === incomingEvolution.baseResourceName,
                );

                if (outgoingEvolutionsOfPreviousNode.length === 1) {
                    const previousNode = evolutionNodes.filter(
                        (n) =>
                            n.stage === incomingEvolution.baseStage &&
                            n.resourceName === incomingEvolution.baseResourceName,
                    )[0];

                    const cy = Math.min(node.cy, previousNode.cy);
                    node.cy = cy;
                    previousNode.cy = cy;
                }
            }
        }

        return evolutionNodes;
    }

    private createRegularEvolutionLink(evolution: Evolution): EvolutionLink {
        const baseNode = this.evolutionNodes().filter(
            (n) => n.stage === evolution.baseStage && n.resourceName === evolution.baseResourceName,
        )[0];
        const evolvedNode = this.evolutionNodes().filter(
            (n) =>
                n.stage === evolution.evolvedStage &&
                n.resourceName === evolution.evolvedResourceName,
        )[0];

        const outgoingEvolutions = this.evolutions()
            .filter(
                (oe) =>
                    oe.baseStage === evolution.baseStage &&
                    oe.baseResourceName === evolution.baseResourceName,
            )
            .filter((e) => e.baseStage !== e.evolvedStage) //Exclude same stage evolutions
            .sort((e1, e2) => e1.evolvedSortIndex - e2.evolvedSortIndex);

        let x0 = 0;
        let y0 = 0;
        let unavailableX = 0;

        const x1 = baseNode.cx + baseNode.r + this.horizontalSpacing / 6;
        const y1 = evolvedNode.cy;
        const x2 = evolvedNode.cx - evolvedNode.r;
        const y2 = evolvedNode.cy;

        if (outgoingEvolutions.length === 1) {
            x0 = baseNode.cx + baseNode.r;
            y0 = baseNode.cy;
            unavailableX = x0 + (x2 - x0) / 2;
        } else {
            //Spread start nodes along arc +/- 45°
            const da = Math.PI / 2 / (outgoingEvolutions.length - 1);
            const index = outgoingEvolutions.indexOf(evolution);
            const a = Math.PI / 4 - index * da;
            x0 = baseNode.cx + baseNode.r * Math.cos(a);
            y0 = baseNode.cy - baseNode.r * Math.sin(a);
            unavailableX = x1 + (x2 - x1) / 2;
        }

        return <EvolutionLink>{
            x0: x0,
            y0: y0,
            x1: x1,
            y1: y1,
            x2: x2,
            y2: y2,
            x3: x2,
            y3: y2,
            textX: y0 === y1 ? x0 + 5 : x1,
            textY: y1 + 10,
            textWidth: y0 === y1 ? x2 - x0 - 10 : x2 - x1 - 5,
            textHeight: this.baseSize,
            unavailableX: unavailableX,
            unavailableY: y2,
            baseResourceName: evolution.baseResourceName,
            evolvedResourceName: evolution.evolvedResourceName,
            evolutionTrigger: evolution.evolutionTrigger,
            isReversible: evolution.isReversible,
            isAvailable: evolution.isAvailable,
        };
    }

    private createSameStageEvolutionLink(evolution: Evolution): EvolutionLink {
        const baseNode = this.evolutionNodes().filter(
            (n) => n.stage === evolution.baseStage && n.resourceName === evolution.baseResourceName,
        )[0];
        const evolvedNode = this.evolutionNodes().filter(
            (n) =>
                n.stage === evolution.evolvedStage &&
                n.resourceName === evolution.evolvedResourceName,
        )[0];

        let x0 = 0;
        const y0 = baseNode.cy;
        let x1 = 0;
        const y1 = baseNode.cy;
        let x2 = 0;
        const y2 = evolvedNode.cy;
        let x3 = 0;
        const y3 = evolvedNode.cy;
        let textX = 0;

        //left-oriented link
        if (evolution.baseStage === 0) {
            x0 = baseNode.cx - baseNode.r;
            x1 = x0 - baseNode.r;
            x2 = x1;
            x3 = evolvedNode.cx - evolvedNode.r;
            textX = x1 - 1.5 * this.baseSize;
        } else {
            //right-oriented link
            x0 = baseNode.cx + baseNode.r;
            x1 = x0 + baseNode.r;
            x2 = x1;
            x3 = evolvedNode.cx + evolvedNode.r;
            textX = x1 + 10;
        }

        return <EvolutionLink>{
            x0: x0,
            y0: y0,
            x1: x1,
            y1: y1,
            x2: x2,
            y2: y2,
            x3: x3,
            y3: y3,
            textX: textX,
            textY: evolvedNode.cy,
            textWidth: 1.5 * this.baseSize - 10,
            textHeight: this.baseSize,
            unavailableX: x2 + (x3 - x2) / 4,
            unavailableY: y3,
            baseResourceName: evolution.baseResourceName,
            evolvedResourceName: evolution.evolvedResourceName,
            evolutionTrigger: evolution.evolutionTrigger,
            isReversible: evolution.isReversible,
            isAvailable: evolution.isAvailable,
        };
    }
}
