import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from 'src/app/common/services/authentication.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  urlSubscription?: Subscription;
  datasource: any;
  user: any;

  public isPictureNull!: boolean;


  constructor(
    private serviceAuth: AuthenticationService,
  ) {
  }

  ngOnInit(): void {

    this.serviceAuth.loggedUserId().subscribe(res => {
      this.serviceAuth.getUserInfo(res.userId).subscribe(res => {
        this.datasource = res;

        if (res.profilePicture == "") {
          this.isPictureNull = true;
        } else {
          this.isPictureNull = false;
        }

      });
    });
  }
}
