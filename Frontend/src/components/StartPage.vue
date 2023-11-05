<template>
  <div class="container">
    <Cart
    class="cart" 
    v-for="object in objects" 
    v-bind:cartobject="object"
    v-bind:displayName="object.displayName"
    v-bind:isOnline="object.isOnline"
    v-bind:avatarUrl="object.avatarUrl"
    v-bind:totalPositiveCount="object.totalPositiveCount"
    v-bind:totalNegativeCount="object.totalNegativeCount"
    v-bind:id="object.id" 
    @click.prevent="openModal(object.id)"
    />
    <Modal v-bind:id="id" v-if="isVisibility" :closefn="closeModal" />
  </div>
</template>

<script>
import Cart from "./Cart.vue"
import Modal from "./Modal.vue"
import axios from 'axios'

export default {

  mounted() {
    axios.get('https://32f8-95-28-137-40.ngrok-free.app/api/users', {
      headers: {
        'ngrok-skip-browser-warning': 'true'
      }
    }).then((res) => {
      this.objects = res.data
    })
  },

  data() {
    return {
      objects: [],
      isVisibility: false,
      id: ""
    };
  },
  components: {
    Cart,
    Modal,
  },
  methods: {
    openModal(id) {
      this.id = id
      this.isVisibility = true
    },
    closeModal() {
       this.isVisibility = false
    }
  },


}

</script>

<style lang="sass" scoped>

.container
  max-width: 1400px
  height: 94vh
  margin: 0 auto
  border: 2px solid #000
  border-radius: 10px
  padding: 20px 110px
  margin-left: 50px
  margin-right: 50px
  display: flex
  flex-wrap: wrap
  overflow: auto
  & .cart
    margin: 15px
    transition: 0.3s
    &:hover
      box-shadow: 10px 5px 5px #FF9573

html * /* override x.xhtml.ru style */ 
  scrollbar-width: thin
  scrollbar-color: blue orange
*::-webkit-scrollbar,
html *::-webkit-scrollbar 
  height: 10px
  width: 10px
*::-webkit-scrollbar-track,
html *::-webkit-scrollbar-track 
  background: #996ca5
*::-webkit-scrollbar-thumb,
html *::-webkit-scrollbar-thumb 
  background: #d99f5f
  border-radius: 5px
  border: 3px solid #d99f5f

</style>