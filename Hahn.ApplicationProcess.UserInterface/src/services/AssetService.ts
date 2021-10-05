import { Asset } from '../models/Asset';
import axiosConfig from '../services/axiosConfig';

const resource: string = '/Assets';

export class AssetService{

    async GetAssets() : Promise<Array<Asset>>{
        const { data } = await axiosConfig.get(`${resource}`);
        return data;
    }

}