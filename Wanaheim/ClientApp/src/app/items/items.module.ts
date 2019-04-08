import { PhotoService } from './../services/photo.service';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemFormComponent } from './components/item-form/item-form.component';
import { RouterModule } from '@angular/router';
import { ItemsComponent } from './components/items/items.component';
import { ItemViewComponent } from './components/item-view/item-view.component';

const routes = [
  {path: '', component: ItemsComponent},
  {path: 'new', component: ItemFormComponent},
  {path: 'new/:id', component: ItemFormComponent},
  {path: ':id', component: ItemViewComponent }
]

@NgModule({
  declarations: [ItemFormComponent, ItemsComponent, ItemViewComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule
  ], 
  providers: [
    PhotoService
  ]
})
export class ItemsModule { }
