import { Component, signal } from '@angular/core';
import { Product } from '../../models/products.model';
import { ProductCardComponent } from './product-card/product-card.component';

@Component({
  selector: 'app-products-list',
  imports: [ProductCardComponent],
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.scss'
})
export class ProductsListComponent {
  products = signal<Product[]>([
    {
      id: 1,
      title: 'Product 1',
      description: 'Description of Product 1',
      price: 10.0,
      image: 'https://via.placeholder.com/150',
      stock: 5
    },
    {
      id: 2,
      title: 'Product 2',
      description: 'Description of Product 2',
      price: 20.0,
      image: 'https://via.placeholder.com/150',
      stock: 0
    },
    {
      id: 3,
      title: 'Product 3',
      description: 'Description of Product 3',
      price: 30.0,
      image: 'https://via.placeholder.com/150'
    }
  ]);
}
