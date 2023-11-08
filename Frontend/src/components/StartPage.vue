<template>
  <div class="center">
    <div class="container" v-if="!isLoading">
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
    <div class="preloader" v-else>
      <Loader />
    </div>
  </div>
</template>

<script>
import Cart from "./Cart.vue";
import Modal from "./Modal.vue";
import Loader from "./Loader.vue";
import axios from "axios";
import { API_BEC_DIS } from '../api';
import { OrderType } from '../api';

export default {
  
  mounted() {
    axios
      .get(API_BEC_DIS+`users?OrderType=${OrderType.ByOnline}`, {
        headers: {
          "ngrok-skip-browser-warning": "true",
        },
      })
      .then((res) => {
        this.objects = res.data;
        setInterval(this.updateState, 2000);
        this.isLoading = false
      });
  },

  data() {
    return {
      objects: [],
      isVisibility: false,
      id: "",
      isLoading: true,
    };
  },

  components: {
    Cart,
    Modal,
    Loader,
  },

  methods: {
    openModal(id) {
      this.id = id;
      this.isVisibility = true;
    },
    closeModal() {
      this.isVisibility = false;
    },
    updateState() {
      axios
        .get(API_BEC_DIS+`users?OrderType=${OrderType.ByOnline}`, {
          headers: {
            "ngrok-skip-browser-warning": "true",
          },
        })
        .then((res) => {
          this.objects = res.data;
        });
    },
  },
};
</script>

<style lang="sass" scoped>
@import url(../assets/colors.sass)

.center
  height: 95vh
  display: flex
  align-items: center
  justify-content: center
.container
  max-width: 1150px
  height: 90vh
  margin: 0 auto
  display: flex
  flex-wrap: wrap
  overflow: auto
  & .cart
    margin: 15px
    transition: 0.3s
    &:hover
      box-shadow: 5px 5px 5px #7d7f7d

html * /* override x.xhtml.ru style */
  scrollbar-width: thin
  scrollbar-color: blue orange
*::-webkit-scrollbar,
html *::-webkit-scrollbar
  height: 10px
  width: 10px
*::-webkit-scrollbar-track,
html *::-webkit-scrollbar-track
  background: #fff
  border-radius: 65px
*::-webkit-scrollbar-thumb,
html *::-webkit-scrollbar-thumb
  background: #fff
  border-radius: 5px
  border: 3px solid #4b4b4b
</style>