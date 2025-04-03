import { Component, inject, signal } from '@angular/core';
import { PrimaryButtonComponent } from "../shared-components/primary-button/primary-button.component";
import { CartService } from '../services/cart.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [PrimaryButtonComponent, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  title = signal('Swift Shop');

  cartService = inject(CartService);

  showCartClicked(){
    console.log('Show cart clicked');
  }
}
