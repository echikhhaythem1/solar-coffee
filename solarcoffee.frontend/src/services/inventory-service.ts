import axios from 'axios';
export class InventoryService{
    API_URL = process.env.VUE_APP_API_URL;

    public async getInventory() : Promise<any>{
        console.log('getInventory:', this.API_URL)
        let result: any = await axios.get(`${this.API_URL}/inventory/`);
    }
}