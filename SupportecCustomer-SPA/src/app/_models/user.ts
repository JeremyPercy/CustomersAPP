import { Photo } from './photo';
import { Company } from './company';
import { Product } from './product';

export interface User {
    id: number;
    email: string;
    created: Date;
    name: string;
    lastName: string;
    photoUrl: string;
    companyName: string;
    phone?: string;
    company?: Company;
    photos?: Photo[];
    products?: Product[];

}
