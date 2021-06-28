import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
  }

  logout(): void {
    sessionStorage.removeItem('token'); 
    this.router.navigate(['/login']);
  }

  isSessionValid(): boolean {
    return this.accountService.isSessionValid();
  }

  getDisplayName(): string {
    return this.accountService.getDisplayName();
  }

}
