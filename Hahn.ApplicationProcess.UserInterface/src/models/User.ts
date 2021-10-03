import { Asset } from './Asset';
export class User{
    
    Id: Number;
    Age: Number;
    FirstName: String;
    LastName: String;
    Email: String;
    Address: String;
    AssetName: String[];
    Assets: Asset[];
    
    constructor({ id, age, firstName, lastName, email, assetname, assets }){

        this.Id = id;
        this.Age = age;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.AssetName = assetname;
        this.Assets = assets;

    }
}