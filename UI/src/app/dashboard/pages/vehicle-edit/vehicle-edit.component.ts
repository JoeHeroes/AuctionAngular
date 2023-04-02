import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from 'src/app/common/services/vehicle.service';

@Component({
  selector: 'app-vehicle-edit',
  templateUrl: './vehicle-edit.component.html',
  styleUrls: ['./vehicle-edit.component.css']
})
export class VehicleEditComponent {
  private returnUrl!: string;

  vehicleForm !: FormGroup;
  errorMessage: string = '';
  showError!: boolean;

  constructor(private service: VehicleService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.vehicleForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
    })
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  addVehicle = (registerFormValue: any) => {
    // this.showError = false;
    // const register = { ...registerFormValue };
    // const userForAuth: UserRegisterDto = {
    //   email: register.email,
    //   password: register.password,
    //   confirmpassword: register.confirmpassword,
    //   firstname: register.firstname,
    //   lastname: register.lastname,
    //   nationality: register.nationality,
    //   dateofbirth: register.dateofbirth,
    //   roleid: register.roleid,

    // }
    // this.service.registerUser(userForAuth)
    //   .subscribe({
    //     next: (res: any) => {
    //       this.router.navigate([this.returnUrl]);
    //     },
    //     error: (err: HttpErrorResponse) => {
    //       this.errorMessage = err.message;
    //       this.showError = true;
    //     }
    //   })
  }

}
