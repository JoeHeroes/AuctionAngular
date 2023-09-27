import { Component } from "@angular/core";
import { AuthenticationService } from "src/app/services/authentication.service";
import { StorageService } from "src/app/services/storage.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  imageUrl: string = '';
  userId: number = 0;
  datasource: any = {
    id: 0,
    name: "",
    sureName: "",
    email: "",
    nationality: "",
    phone: "",
    profilePicture: "",
  };
  isPictureNull!: boolean;

  constructor(private authenticationService: AuthenticationService,
    private storageService: StorageService) {

    this.authenticationService.loggedUserId().subscribe(res => {
      this.userId = res.userId;
      this.authenticationService.getUserInfo(res.userId).subscribe(res => {
        this.datasource = res;
        if (res.profilePicture == "") {
          this.isPictureNull = true;
          this.imageUrl = this.storageService.getProfileImageUrl("profileDefult");
        } 
        else 
        {
          this.isPictureNull = false;
          this.imageUrl = this.storageService.getProfileImageUrl(res.profilePicture);
        }
      });
    });

  }
}