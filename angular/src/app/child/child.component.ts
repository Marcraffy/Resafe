import { Component, OnInit, Input, Injector } from '@angular/core';
import { ChildDto } from 'shared/service-proxies/service-proxies';
import { AppComponentBase } from 'shared/app-component-base';

@Component({
    selector: 'child',
    templateUrl: 'child.component.html',
})

export class ChildComponent extends AppComponentBase {

    @Input() child: ChildDto = new ChildDto();
    @Input() isShown: boolean;
    @Input() isOutOfBounds: boolean = false;
    public show: boolean = false;

    constructor(injector: Injector)
    {
        super(injector);
        this.child.name = ""; 
        this.child.longitude = 0; 
        this.child.latitude = 0; 
    }
}