import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { IEvolutionModel } from '../../models/evolution.model';
import { EvolutionLink } from './core/evolution-link';
import { EvolutionNode } from './core/evolution-node';

@Component({
    selector: 'pokeone-evolution-chart',
    templateUrl: './evolution-chart.component.html',
    styleUrls: ['./evolution-chart.component.scss']
})
export class EvolutionChartComponent implements OnInit {

    @Input() evolutions: IEvolutionModel[] = []

    baseSize: number = 100;
    horizontalSpacing: number = this.baseSize * 2;
    verticalSpacing: number = this.baseSize * 0.5;
    arrowLength: number = 10;
    arrowWidth: number = 6;
    leftPad: number = 50;
    rightPad: number = 50;
    topPad: number = 5;
    bottomPad: number = 20;

    stages: number[] = []
    maxStageSize: number = 0;

    evolutionNodes: EvolutionNode[] = [];
    evolutionLinks: EvolutionLink[] = [];

    constructor(private router: Router) { }

    ngOnInit(): void {
        this.checkHasSameStageEvolutions();
        this.setStages();
        this.setMaxStageSize();
        this.createEvolutionNodes();
        this.createEvolutionLinks();
    }

    getTotalWidth(): number {
        return this.stages.length * this.baseSize + (this.stages.length - 1) * this.horizontalSpacing + this.leftPad + this.rightPad;
    }

    getTotalHeight(): number {
        return this.maxStageSize * (this.baseSize + this.verticalSpacing) + this.topPad + this.bottomPad;
    }

    navigate(url: string) {
        this.router.navigateByUrl(url);
    }

    private checkHasSameStageEvolutions() {
        let sameStageEvolutions = this.evolutions.filter(e => e.baseStage === e.evolvedStage);
        if (sameStageEvolutions.length > 0) {
            if (sameStageEvolutions[0].baseStage === 0) {
                this.leftPad += 2 * this.baseSize
            }
            else {
                this.rightPad += 2 * this.baseSize
            }
        }
    }

    private setStages() {
        var baseStages = this.evolutions.map(e => e.baseStage);
        var evolvedStages = this.evolutions.map(e => e.evolvedStage);

        var distinctStages = [...new Set(baseStages.concat(evolvedStages))]
        this.stages = distinctStages;
    }

    private setMaxStageSize() {
        var maxCount = 0;

        this.stages.forEach((stage: number) => {
            var stageEvolutions = this.evolutions.filter(e => e.evolvedStage === stage);
            var distinctStageVarieties = [...new Set(stageEvolutions.map(e => e.evolvedResourceName))];
            var count = distinctStageVarieties.length;
            if (count > maxCount) {
                maxCount = count;
            }
        });

        this.maxStageSize = maxCount;
    }

    private createEvolutionNodes() {
        var basePokemon = this.evolutions
            .map(e => <EvolutionNode> {
                name: e.baseName,
                resourceName: e.baseResourceName,
                spriteName: e.baseSpriteName,
                type1: e.basePrimaryElementalType,
                type2: e.baseSecondaryElementalType,
                stage: e.baseStage,
                sortIndex: e.baseSortIndex,
                cx: 0, cy: 0, r: this.baseSize / 2
            });

        var evolvedPokemon = this.evolutions
            .map(e => <EvolutionNode> {
                name: e.evolvedName,
                resourceName: e.evolvedResourceName,
                spriteName: e.evolvedSpriteName,
                type1: e.evolvedPrimaryElementalType,
                type2: e.evolvedSecondaryElementalType,
                stage: e.evolvedStage,
                sortIndex: e.evolvedSortIndex,
                cx: 0, cy: 0, r: this.baseSize / 2
            });

        var allPokemon = basePokemon.concat(evolvedPokemon);

        var distinctPokemon: EvolutionNode[] = [];

        allPokemon.forEach(p => {
            if (!distinctPokemon.map(dp => dp.resourceName).includes(p.resourceName)) {
                distinctPokemon.push(p);
            }
        })

        this.evolutionNodes = distinctPokemon.sort((p1, p2) => {
            if (p1.stage != p2.stage) {
                return p1.stage - p2.stage
            }
            else {
                return p1.sortIndex - p2.sortIndex
            }
        })

        this.placeEvolutionNodes();
    }

    private placeEvolutionNodes() {
        var maxStage = Math.max(...this.stages);
        for (let stage = 0; stage <= maxStage; stage++) {
            let nodes = this.evolutionNodes
                .filter(n => n.stage === stage)
                .sort((n1, n2) => n1.sortIndex - n2.sortIndex);

            let stageHeight = nodes.length * (this.baseSize + this.verticalSpacing)
            let y0 = this.topPad + (this.getTotalHeight() - stageHeight) / 2 + (this.baseSize / 2)
            let x0 = this.leftPad + this.baseSize / 2;

            for (let i = 0; i < nodes.length; i++) {
                let node = nodes[i];
                node.cy = y0 + i * (this.baseSize + this.verticalSpacing);
                node.cx = x0 + stage * (this.baseSize + this.horizontalSpacing);

                //If the current node only has one incoming evolution, and the previous node
                //by evolution has only one outgoing evolution, make sure that the two nodes are on the
                //same height.
                let incomingEvolutions = this.evolutions
                    .filter(e => e.evolvedStage === node.stage && e.evolvedResourceName === node.resourceName)
                    .filter(e => e.baseStage !== e.evolvedStage); //Exclude same stage evolutions

                if (incomingEvolutions.length !== 1) {
                    continue;
                } 

                let incomingEvolution = incomingEvolutions[0];
                let outgoingEvolutionsOfPreviousNode = this.evolutions
                    .filter(e => e.baseStage === incomingEvolution.baseStage && e.baseResourceName === incomingEvolution.baseResourceName);

                if (outgoingEvolutionsOfPreviousNode.length === 1) {
                    let previousNode = this.evolutionNodes
                        .filter(n => n.stage === incomingEvolution.baseStage && n.resourceName === incomingEvolution.baseResourceName)[0];

                    let cy = Math.min(node.cy, previousNode.cy);
                    node.cy = cy;
                    previousNode.cy = cy;
                }
            }
        }
    }

    private createEvolutionLinks() {
        this.evolutionLinks = this.evolutions.map(e => {
            
            //Same stage evolutions are different
            if (e.baseStage === e.evolvedStage) {
                return this.createSameStageEvolutionLink(e);
            }
            else {
                return this.createRegularEvolutionLink(e);
            }
        })
    }

    private createRegularEvolutionLink(evolution: IEvolutionModel): EvolutionLink {
        let baseNode = this.evolutionNodes
            .filter(n => n.stage === evolution.baseStage && n.resourceName === evolution.baseResourceName)[0];
        let evolvedNode = this.evolutionNodes
            .filter(n => n.stage === evolution.evolvedStage && n.resourceName === evolution.evolvedResourceName)[0];

        let outgoingEvolutions = this.evolutions
            .filter(oe => oe.baseStage === evolution.baseStage && oe.baseResourceName === evolution.baseResourceName)
            .filter(e => e.baseStage !== e.evolvedStage) //Exclude same stage evolutions
            .sort((e1, e2) => e1.evolvedSortIndex - e2.evolvedSortIndex)

        let x0 = 0;
        let y0 = 0;
        let unavailableX = 0;

        let x1 = baseNode.cx + baseNode.r + (this.horizontalSpacing / 6);
        let y1 = evolvedNode.cy;
        let x2 = evolvedNode.cx - evolvedNode.r;
        let y2 = evolvedNode.cy;

        if (outgoingEvolutions.length === 1) {
            x0 = baseNode.cx + baseNode.r;
            y0 = baseNode.cy;
            unavailableX = x0 + ((x2 - x0) / 2);
        }
        else {
            //Spread start nodes along arc +/- 45°
            let da = (Math.PI / 2) / (outgoingEvolutions.length - 1)
            let index = outgoingEvolutions.indexOf(evolution);
            let a = (Math.PI / 4) - (index * da);
            x0 = baseNode.cx + baseNode.r * Math.cos(a);
            y0 = baseNode.cy - baseNode.r * Math.sin(a);
            unavailableX = x1 + ((x2 - x1) / 2);
        }

        return <EvolutionLink> {
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
            isAvailable: evolution.isAvailable
        }
    }

    private createSameStageEvolutionLink(evolution: IEvolutionModel): EvolutionLink {
        let baseNode = this.evolutionNodes
            .filter(n => n.stage === evolution.baseStage && n.resourceName === evolution.baseResourceName)[0];
        let evolvedNode = this.evolutionNodes
            .filter(n => n.stage === evolution.evolvedStage && n.resourceName === evolution.evolvedResourceName)[0];

        let x0 = 0;
        let y0 = baseNode.cy;
        let x1 = 0;
        let y1 = baseNode.cy;
        let x2 = 0;
        let y2 = evolvedNode.cy;
        let x3 = 0;
        let y3 = evolvedNode.cy;
        let textX = 0;

        //left-oriented link
        if (evolution.baseStage === 0) {
            x0 = baseNode.cx - baseNode.r;
            x1 = x0 - baseNode.r;
            x2 = x1;
            x3 = evolvedNode.cx - evolvedNode.r;
            textX = x1 - (1.5 * this.baseSize);
        }
        //right-oriented link
        else {
            x0 = baseNode.cx + baseNode.r;
            x1 = x0 + baseNode.r;
            x2 = x1;
            x3 = evolvedNode.cx + evolvedNode.r;
            textX = x1 + 10
        }

        return <EvolutionLink> {
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
            textWidth: (1.5 * this.baseSize) - 10,
            textHeight: this.baseSize,
            unavailableX: x2 + ((x3 - x2) / 4),
            unavailableY: y3,
            baseResourceName: evolution.baseResourceName,
            evolvedResourceName: evolution.evolvedResourceName,
            evolutionTrigger: evolution.evolutionTrigger,
            isReversible: evolution.isReversible,
            isAvailable: evolution.isAvailable
        }
    }
}
