import { Injectable, signal } from '@angular/core';
import { Product } from '../models/products.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cart = signal<Product[]>([]);

  addToCart(product: Product) {
    this.cart.update(cart => [...cart, product]);
  }

  removeFromCart(id: number) {
    this.cart.update(cart => cart.filter(item => item.id !== id));
  }

  constructor() { }
}
