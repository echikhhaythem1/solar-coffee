<template>
    <div class="inventory-container">
        <h1 id="inventoryTitle">
            Inventory Dashboard
        </h1>
        <hr/>
        <div class="inventory-actions">
            <solar-button @click.native="showNewProductModel" id="addNewBtn">
                Add New Item
            </solar-button>
            <solar-button @click.native="showShipmentModel" id="receiveShipmentBtn">
                Receive Shipment
            </solar-button>
        </div>

        <table id="inventoryTable" class="table">
            <tr>
                <th>Item</th>
                <th>Quantity On-Hand</th>
                <th>Unit Price</th>
                <th>Taxable</th>
                <th>Delete</th>
            </tr>
                <tr v-for="item in inventory" :key="item.id">
                    <td>{{item.product.name}}</td>
                     <td>{{item.quantityOnHand}}</td>
                      <td>{{item.product.price | price}}</td>
                     <td><span v-if="item.product.isTaxable"> Yes
                         </span> 
                        <span v-else>
                            No
                        </span>
                     </td>
                     <td>
                         <div>
                             x
                         </div>
                     </td>
                </tr>
        </table>
        <new-product-modal v-if="isNewProductVisible" @save:product="saveNewProduct" @close="closeModals" />
        <shipment-model v-if="isShipmentVisible" :inventory="inventory" @save:shipment="saveNewShipment" @close="closeModals" />
    </div>
</template>
<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import {IProduct, IProductInventory} from '@/types/Product';
import SolarButton from '@/components/SolarButton.vue';
import NewProductModal from '@/components/Models/NewProductModal.vue';
import ShipmentModel from '@/components/Models/ShipmentModel.vue';
import { IShipment } from '@/types/Shipment';
import { InventoryService } from "@/services/inventory-service";

const inventoryService = new InventoryService();
@Component({
  name: 'Inventory',
  components:{SolarButton, NewProductModal, ShipmentModel}
})

export default class Inventory extends Vue {
    isNewProductVisible:boolean=false;
    isShipmentVisible:boolean=false;
    inventory : IProductInventory[]=[];
    closeModals(){
        this.isShipmentVisible = false;
        this.isNewProductVisible = false;
    }
showNewProductModel(){
    this.isNewProductVisible = true;
}
showShipmentModel(){
    this.isShipmentVisible = true;
} 
saveNewProduct(newProduct: IProduct){
    console.log("saveNewProduct:");
    console.log(newProduct);
}
saveNewShipment(shipment:IShipment){ 
    console.log("saveNewShipment:");
    console.log(shipment)
}
async fetchData(){
  this.inventory = await inventoryService.getInventory()
}
async created(){
    await this.fetchData();
}
}
</script>
<style scoped>

</style>