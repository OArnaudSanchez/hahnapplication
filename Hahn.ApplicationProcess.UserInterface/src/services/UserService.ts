import { User } from '../models/User';
import axiosConfig from '../services/axiosConfig';

const resource: string = '/Users';

export class UserService{

    async GetUser(idUser: Number) : Promise<User>{
        const { data } = await axiosConfig.get(`${resource}/${idUser}`);
        return data;
    }

    async AddUser(user : User) : Promise<any>{
        const result =  await axiosConfig.post(`${resource}`, this.getUserData(user));
        return result;
    }

    async UpdateUser(user: User) : Promise<void>{
        return await axiosConfig.put(`${resource}`, this.getUserData(user));
    }

    async DeleteUser(id: Number) : Promise<void> {
        return await axiosConfig.delete(`${resource}/${id}`);
    }

    getUserData(User: User) : Object {

        const data = {
            "id": Number(User.Id),
            "age": Number(User.Age),
            "firstname": User.FirstName,
            "lastname": User.LastName,
            "email": User.Email,
            "address": User.Address,
            "assetName": User.AssetName.map(x => x.toLowerCase()) 
        };

        return data;
    }
}