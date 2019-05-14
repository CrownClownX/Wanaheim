import { PhotoService } from './../../../services/photo.service';
import { ItemsService } from './../../../services/items.service';
import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-item-view',
    templateUrl: './item-view.component.html',
    styleUrls: ['./item-view.component.styl']
})
export class ItemViewComponent implements OnInit {

    item: any;
    itemId: number;
    @ViewChild('fileInput') fileInput: ElementRef;
    photos: any[];
    progress: any[];
    profile: any;

    /** item-view ctor */
    constructor(
        private service: ItemsService,
        private route: ActivatedRoute,
        private router: Router,
        private photoService: PhotoService
    ) {
        route.params.subscribe(p => {
            this.itemId = +p['id'];

            if (isNaN(this.itemId) || this.itemId == 0) {
                router.navigate(['/items']);
                return;
            }
        });

    }

    ngOnInit() {
        this.photoService.getPhotos(this.itemId)
            .subscribe(photos => this.photos = photos);

        this.service.getItem(this.itemId)
            .subscribe(item => this.item = item);
    }

    onClickRedirection() {
        this.router.navigate(['/items/new/' + this.item.id]);
    }

    onClickDelete() {
        if (confirm("Are you sure?")) {
            this.service.delete(this.item.id)
                .subscribe(x => {
                    this.router.navigate(['/home']);
                });
        }
    }

    upload() {
        var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
        var file = nativeElement.files[0];
        nativeElement.value = '';

        this.photoService.upload(this.itemId, file)
            .subscribe(photo => {
                this.photos.push(photo)
            },
            err => {
                console.log(err);
            }
        );
    }

}
