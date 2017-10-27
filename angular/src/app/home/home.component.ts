import { Component, Injector, OnInit, Input, ApplicationRef } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ChildServiceProxy, ChildDto } from 'shared/service-proxies/service-proxies';

@Component({
    templateUrl: './home.component.html',
    animations: [appModuleAnimation()]
})

export class HomeComponent extends AppComponentBase implements OnInit {

    @Input() children: ChildDto[] = new Array<ChildDto>();
    private child: ChildDto = new ChildDto();

    private isParent: boolean = false;

    constructor(
        injector: Injector,
        private proxy: ChildServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        abp.ui.setBusy();

        this.isParent = abp.auth.isGranted("Pages.Parent");
        
        this.proxy.getChildren().subscribe(
            next => {
                this.children = next.sort((a, b) => a.name.includes("Patrick") ? -1 : 0);
                this.child = new ChildDto({name: "Jay Hunter", latitude: -29.821353, longitude: 30.956933})
                abp.ui.clearBusy();
            }
        )
    }
}