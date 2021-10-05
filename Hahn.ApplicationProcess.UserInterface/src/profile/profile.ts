import { inject } from 'aurelia-framework';
import { UserService } from '../services/UserService';
import { User } from '../models/User';

@inject(UserService, User)

export class Profile{
    
    userService: UserService;
    user: User;

    constructor( userService, user ){
        this.userService = userService;
        this.user = user;
    }

    async activate({ id }){
       const user = await this.userService.GetUser(id);
       this.user = user;
       console.log(this.user)
    }
}