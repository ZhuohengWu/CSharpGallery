import { Component, signal } from '@angular/core';
import { Product } from '../../models/products.model';

@Component({
  selector: 'app-products-list',
  imports: [],
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
      image: 'https://via.placeholder.com/150'
    },
    {
      id: 2,
      title: 'Product 2',
      description: 'Description of Product 2',
      price: 20.0,
      image: 'https://via.placeholder.com/150'
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
