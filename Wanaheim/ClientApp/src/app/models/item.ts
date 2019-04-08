import { Data } from "@angular/router";

export interface KeyValuePair {
    id: number;
    name: string;
}

export interface Item {
    id: number;
    name: string;
    price: number;
    quantity: number;
    description: string;
    creationDate: Data;

    category: KeyValuePair;
    subcategory: KeyValuePair;
}

export interface ItemSave {
    id: number;
    name: string;
    price: number;
    quantity: number;
    description: string;

    categoryId: number;
    subcategoryId: number;
}