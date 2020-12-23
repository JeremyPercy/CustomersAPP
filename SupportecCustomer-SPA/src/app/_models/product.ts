import { Item } from './item';

export interface Product {
    id?: number;
    name: string;
    photoUrl: string;
    userId: number;
    items?: Item[];
}
