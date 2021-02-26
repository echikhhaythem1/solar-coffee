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
@Component({
  name: 'Inventory',
  components:{SolarButton, NewProductModal, ShipmentModel}
})

export default class Inventory extends Vue {
    isNewProductVisible:boolean=false;
    isShipmentVisible:boolean=false;
    inventory : IProductInventory[]=[
        {
            id:1,
            product:{
                id: 1,
                createdOn:new Date(),
                updateOn:new Date(),
                name:"Some Product",
                description:"Good stuff",
                price:100,
                isTaxable: false,
                isArchived: false
            },
                quantityOnHand:100,
                idealQuantity: 100
        },
         {
            id:2,
            product:{
                id: 2,
                createdOn:new Date(),
                updateOn:new Date(),
                name:"Another Product",
                description:"Good stuff",
                price:100,
                isTaxable: true,
                isArchived: false
            },
                quantityOnHand:200,
                idealQuantity: 200
        }
        
         
    ];
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
}
</script>
<style scoped>

</style>