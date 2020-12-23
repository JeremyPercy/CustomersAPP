import { Component, OnInit, TemplateRef, EventEmitter, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Product } from 'src/app/_models/product';
import { AuthService } from 'src/app/_services/auth.service';
import { ProductService } from 'src/app/_services/product.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  @Output() parentEvent = new EventEmitter();
  modalRef: BsModalRef;
  model: any = {};
  product: any = {};

  // tslint:disable-next-line: max-line-length
  constructor(private modalService: BsModalService, private productService: ProductService, private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template,
      Object.assign({}, { class: 'gray modal-lg text-dark' }));
  }


  ngOnInit() {
  }

  reloadProducts() {
    this.parentEvent.emit();
  }

  addProduct() {
    this.product = {
      name: this.model.name,
      photoUrl: this.model.photoUrl,
      userId: this.authService.decodedToken.nameid
    };
    this.productService.addProduct(this.product).subscribe(() => {
      this.alertify.success('product added successful');
    }, error => {
      this.alertify.error(error);
    });
  }

}
