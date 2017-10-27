import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'map',
    templateUrl: 'map.component.html',
    styleUrls: ['map.component.less'],
})

export class MapComponent {

    @Input() Latitude: number;
    @Input() Longitude: number;

    
}