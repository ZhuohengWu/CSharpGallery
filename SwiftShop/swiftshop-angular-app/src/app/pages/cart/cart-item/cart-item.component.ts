import { Component, input, inject } from '@angular/core';
import { Product } from '../../../models/products.model';
import { ButtonComponent } from "../../../shared-components/button/button.component";
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-cart-item',
  imports: [ButtonComponent],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
  cartService = inject(CartService);
  item = input.required<Product>()
}
