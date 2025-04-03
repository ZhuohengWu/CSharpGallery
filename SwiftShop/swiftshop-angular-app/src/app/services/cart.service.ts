import { Injectable, signal } from '@angular/core';
import { Product } from '../models/products.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart = signal<Product[]>([{
    id: 2,
    title: 'Product 2',
    description: 'Description of Product 2',
    price: 20.0,
    image: 'https://via.placeholder.com/150',
    stock: 0
  }]);

  addToCart(product: Product) {
    this.cart.update(cart => [...cart, product]);
  }

  removeFromCart(id: number) {
    this.cart.update(cart => cart.filter(item => item.id !== id));
  }

  constructor() { }
}
