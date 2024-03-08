import { Component, HostListener } from '@angular/core';
import { UserService } from 'src/app/shared/userServiceService/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(public userService:UserService){}
}
