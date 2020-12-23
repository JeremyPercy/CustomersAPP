import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  products: Product[];
  userId = this.authService.decodedToken.nameid;

  constructor(private productService: ProductService, private alertify: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts(this.userId).subscribe((products: Product[]) => {
      this.products = products;
    }, error => {
      this.alertify.error(error);
    });
  }

}
