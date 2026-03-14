import { Component, signal } from '@angular/core';

@Component({
    selector: 'pokeoneweb-scroll-to-top',
    imports: [],
    templateUrl: './scroll-to-top.html',
    styleUrl: './scroll-to-top.scss',
    host: { '(window:scroll)': 'onWindowScroll()' },
})
export class ScrollToTopComponent {
    windowScrolled = signal(false);
    scrollPosition = signal(0);

    onWindowScroll() {
        this.scrollPosition.set(document.documentElement.scrollTop);
        if (window.pageYOffset || document.documentElement.scrollTop > 100) {
            this.windowScrolled.set(true);
        } else if (
            (this.windowScrolled() && window.pageYOffset) ||
            document.documentElement.scrollTop < 10
        ) {
            this.windowScrolled.set(false);
        }
    }

    scrollToTop() {
        (function smoothscroll() {
            const currentScroll = document.documentElement.scrollTop;
            let targetScroll = currentScroll - currentScroll / 8;
            if (targetScroll < 10) {
                targetScroll = 0;
            }
            if (currentScroll > 0) {
                window.requestAnimationFrame(smoothscroll);
                window.scrollTo(0, targetScroll);
            }
        })();
    }
}
