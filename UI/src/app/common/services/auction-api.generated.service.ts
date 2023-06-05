
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from as _observableFrom, throwError as _observableThrow, of as _observableOf, Observable } from 'rxjs';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';



@Injectable({
    providedIn: 'root'
})
export class AuctionSystemApiClient {
    private baseUrl: string;

    constructor(http: HttpClient) {
        this.baseUrl = "https://localhost:7257";
    }
}


export enum Highlight {
    NonOperational = "NonOperational",
    RunAndDrive = "RunAndDrive",
}


export class Locations implements ILocations {

    /** Primary key. */
    id?: string;
    /** Name. */
    name?: string;
    /** Phone. */
    phone?: string;
    /** Email. */
    email?: string;
    /** City. */
    city?: string;
    /** Street. */
    street?: string;
    /** Secondary Damage. */
    postalCode?: string;
    /** ProfileImg. */
    profileImg?: string;

    constructor(data?: ILocations) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.phone = _data["phone"];
            this.email = _data["email"];
            this.city = _data["city"];
            this.street = _data["street"];
            this.postalCode = _data["postalCode"];
            this.profileImg = _data["profileImg"];
        }
    }

    static fromJS(data: any): Locations {
        data = typeof data === 'object' ? data : {};
        let result = new Locations();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["phone"] = this.phone;
        data["email"] = this.email;
        data["city"] = this.city;
        data["street"] = this.street;
        data["postalCode"] = this.postalCode;
        data["profileImg"] = this.profileImg;


        return data;
    }
}

export interface ILocations {

    /** Primary key. */
    id?: string;
    /** Name. */
    name?: string;
    /** Phone. */
    phone?: string;
    /** Email. */
    email?: string;
    /** City. */
    city?: string;
    /** Street. */
    street?: string;
    /** Secondary Damage. */
    postalCode?: string;
    /** ProfileImg. */
    profileImg?: string;
}





export class Vehicle implements IVehicle {
    /** Primary key. */
    id?: number;
    /** Producer. */
    producer?: string;
    /** Model Specifer. */
    modelSpecifer?: string;
    /** Model Generation. */
    modelGeneration?: string;
    /** Registration Year. */
    registrationYear?: number;
    /** Color. */
    color?: string;
    /** BodyType. */
    bodyType?: string;
    /** EngineCapacity. */
    engineCapacity?: number;
    /** engineOutput. */
    engineOutput?: number;
    /** Transmission. */
    transmission?: string;
    /** Drive. */
    drive?: string;
    /** MeterReadout. */
    meterReadout?: number;
    /** Fuel. */
    fuel?: string;
    /** Number Keys. */
    numberKeys?: string;
    /** Service Manual. */
    serviceManual?: boolean;
    /** Second Tire Set. */
    secondTireSet?: boolean;
    /** Create By Id. */
    createById?: number;
    /** Profile Img. */
    profileImg?: string;
    /** Location. */
    location?: string;
    /** Primary Damage. */
    primaryDamage?: string;
    /** Secondary Damage. */
    secondaryDamage?: string;
    /** VIN. */
    vin?: string;
    /** Highlight. */
    highlight?: string;
    /** Date Time. */
    dateTime?: string;
    /** Bid Status. */
    bidStatus?: boolean;
    /** Current Bid. */
    currentBid?: number;
    /** Winner Id. */
    winnerId?: number;
    /** Watch. */
    watch?: boolean;

    constructor(data?: IVehicle) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.producer = _data["producer"];
            this.modelSpecifer = _data["modelSpecifer"];
            this.modelGeneration = _data["modelGeneration"];
            this.registrationYear = _data["registrationYear"];
            this.color = _data["color"];
            this.engineCapacity = _data["engineCapacity"];
            this.engineOutput = _data["engineOutput"];
            this.transmission = _data["transmission"];
            this.drive = _data["drive"];
            this.meterReadout = _data["meterReadout"];
            this.fuel = _data["fuel"];
            this.numberKeys = _data["numberKeys"];
            this.serviceManual = _data["serviceManual"];
            this.secondTireSet = _data["secondTireSet"];
            this.createById = _data["createById"];
            this.profileImg = _data["profileImg"];
            this.location = _data["location"];
            this.primaryDamage = _data["primaryDamage"];
            this.secondaryDamage = _data["secondaryDamage"];
            this.vin = _data["vin"];
            this.highlight = _data["highlight"];
            this.dateTime = _data["dateTime"];
            this.bidStatus = _data["bidStatus"];
            this.currentBid = _data["currentBid"];
            this.winnerId = _data["winnerId"];
            this.watch = _data["watch"];
        }
    }

    static fromJS(data: any): Vehicle {
        data = typeof data === 'object' ? data : {};
        let result = new Vehicle();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["producer"] = this.producer;
        data["modelSpecifer"] = this.modelSpecifer;
        data["modelGeneration"] = this.modelGeneration;
        data["registrationYear"] = this.registrationYear;
        data["color"] = this.color;
        data["engineCapacity"] = this.engineCapacity;
        data["engineOutput"] = this.engineOutput;
        data["transmission"] = this.transmission;
        data["drive"] = this.drive;
        data["meterReadout"] = this.meterReadout;
        data["fuel"] = this.fuel;
        data["numberKeys"] = this.numberKeys;
        data["serviceManual"] = this.serviceManual;
        data["secondTireSet"] = this.secondTireSet;
        data["createById"] = this.createById;
        data["profileImg"] = this.profileImg;
        data["location"] = this.location;
        data["primaryDamage"] = this.primaryDamage;
        data["secondaryDamage"] = this.secondaryDamage;
        data["vin"] = this.vin;
        data["highlight"] = this.highlight;
        data["dateTime"] = this.dateTime;
        data["bidStatus"] = this.bidStatus;
        data["currentBid"] = this.currentBid;
        data["winnerId"] = this.winnerId;
        data["watch"] = this.watch;

        return data;
    }
}

export interface IVehicle {
    /** Primary key. */
    id?: number;
    /** Producer. */
    producer?: string;
    /** Model Specifer. */
    modelSpecifer?: string;
    /** Model Generation. */
    modelGeneration?: string;
    /** Registration Year. */
    registrationYear?: number;
    /** Color. */
    color?: string;
    /** BodyType. */
    bodyType?: string;
    /** EngineCapacity. */
    engineCapacity?: number;
    /** engineOutput. */
    engineOutput?: number;
    /** Transmission. */
    transmission?: string;
    /** Drive. */
    drive?: string;
    /** MeterReadout. */
    meterReadout?: number;
    /** Fuel. */
    fuel?: string;
    /** Number Keys. */
    numberKeys?: string;
    /** Service Manual. */
    serviceManual?: boolean;
    /** Second Tire Set. */
    secondTireSet?: boolean;
    /** Create By Id. */
    createById?: number;
    /** Profile Img. */
    profileImg?: string;
    /** Location. */
    location?: string;
    /** Primary Damage. */
    primaryDamage?: string;
    /** Secondary Damage. */
    secondaryDamage?: string;
    /** VIN. */
    vin?: string;
    /** Highlight. */
    highlight?: string;
    /** Date Time. */
    dateTime?: string;
    /** Bid Status. */
    bidStatus?: boolean;
    /** Current Bid. */
    currentBid?: number;
    /** Winner Id. */
    winnerId?: number;
    /** Watch. */
    watch?: boolean;
}



function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new TecsApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}




export class TecsApiException {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isTecsApiException = true;

    static isTecsApiException(obj: any): obj is TecsApiException {
        return obj.isTecsApiException === true;
    }
}


export class UserCredentials implements IUserCredentials {
    /** Username. */
    username?: string;
    /** Password. */
    password?: string;

    constructor(data?: IUserCredentials) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.username = _data["username"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): UserCredentials {
        data = typeof data === 'object' ? data : {};
        let result = new UserCredentials();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["username"] = this.username;
        data["password"] = this.password;
        return data;
    }
}

export interface IUserCredentials {
    /** Username. */
    username?: string;
    /** Password. */
    password?: string;
}