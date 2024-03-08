import { Component, OnInit } from '@angular/core';
import { JWTToken } from 'src/app/shared/model/JWT';
import { UserService } from 'src/app/shared/userServiceService/user.service';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  userName: string;
  role: string;
  items: MenuItem[];
  display: string;

  constructor(private userService: UserService, private route: Router) {
    this.initializeValues();
    this.userService.userChange.subscribe((data) => {
      this.userName = data[0];
      this.role = data[1];
      this.display = this.userName + ' (' + this.role + ')';
    });
  }

  ngOnInit(): void {

    this.items = [
      {
        label: '',
        icon: 'pi pi-user',
        items: [
          {
            label: 'Logout',
            command: (event) => {
              this.logoutFunction();
            },
          },
        ],
      },
    ];
  }

  initializeValues() {
    this.userName = this.userService.userName;
    this.role = this.userService.role;
  }

  logoutFunction() {
    this.userService.logOut();
    this.route.navigate(['/authentication/login']);
    this.userName = undefined;
    this.role = undefined;
  }
}
