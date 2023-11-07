<template>
  <div class="cart">
    <div class="cart-state">
      <div class="online" v-if="!isOnline">
        <div class="cart-state_red-circle"></div>
        <p>Offline</p>
      </div>
      <div class="online" v-else>
        <div class="cart-state_green-circle"></div>
        <p>Online</p>
      </div>
    </div> 
    <div class="cart-img">
      <img :src="avatarUrl" alt="">
    </div>
    <h2 class="cart-title"><span class="color">{{ displayName }}</span></h2>
    <div class="cart-likes">
        <button class="cart-likes_like style">
          <img class="" src="/src/assets/like.svg" alt="">{{ totalPositiveCount }}
        </button>
        <button class="cart-likes_dislike style">
          <img class="" src="/src/assets/dislike.svg" alt="">{{ totalNegativeCount }}
        </button>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'

export default {
  props: {
    displayName: String,
    isOnline: Boolean,
    id: String,
    avatarUrl: String,
    totalPositiveCount: Number,
    totalNegativeCount: Number
  },
  data() {
    return {};
  },
  setup() {
    return {
      count: ref(0),
      counts: ref(0),
    };
  },
};
</script>

<style lang="sass" scoped>
@import url(../assets/colors.sass)

.color
  color: #4b4b4b
  font-weight: 400
.style
  border: 0
  border-radius: 10px
  background-color: rgba(255,0,0,0)
  cursor: pointer
.cart
  position: relative
  border-radius: 10px
  display: flex
  flex-direction: column
  justify-content: center
  align-items: center
  width: 230px
  height: 300px
  background: var(--cart)
  &-title
    text-align: center
    margin-bottom: 10px
    margin-top: 10px
  &-img
    width: 80px
    height: 80px
    border-radius: 100%
    background: #15232d
    margin-bottom: 10px
    & img
      border-radius: 100%
  &-state
    position: absolute
    top: 20px
    & .online
      display: flex
      align-items: center
    &_red-circle
      width: 10px
      height: 10px
      background: red
      border-radius: 100%
      margin-left: 10px
      margin-right: 5px
    &_green-circle
      width: 10px
      height: 10px
      background: green
      border-radius: 100%
      margin-left: 10px
      margin-right: 5px
  &-likes
    display: flex
    margin-top: 5px
    margin-bottom: -20px
    &_like
      margin-right: 10px
      display: flex
      width: 70px
      & img
        width: 25px
        margin-right: 10px
    &_dislike
      margin-right: -8px
      width: 70px
      display: flex
      & img
        width: 25px
        margin-right: 10px
</style>