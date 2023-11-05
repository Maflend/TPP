<template>
  <div class="cart">
    <div class="cart-img">
      <img :src="avatarUrl" alt="">
    </div>
    <h2 class="cart-title"><span class="color">{{ displayName }}</span></h2>
    <div class="cart-state">
      <div v-if="!isOnline" class="cart-state_red-circle"></div>
      <div v-else class="cart-state_green-circle"></div>
    </div>
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
.color
  color: #086CA2
.style
  border: 0
  border-radius: 10px
  background-color: rgba(255,0,0,0)
  cursor: pointer
.cart
  position: relative
  border: 2px solid #000
  border-radius: 10px
  display: flex
  flex-direction: column
  justify-content: center
  align-items: center
  width: 250px
  height: 250px
  background: #d99f5f  
  &-img
    width: 80px
    height: 80px
    border-radius: 100%
    background: #15232d
    margin-bottom: 10px
    & img
      border-radius: 100%
  &-title
  &-state
    position: absolute
    right: 20px
    top: 20px
    &-text
    &_red-circle
      width: 10px
      height: 10px
      background: red
      border-radius: 100%
      margin-left: 10px
    &_green-circle
      width: 10px
      height: 10px
      background: green
      border-radius: 100%
      margin-left: 10px
  &-likes
    display: flex
    margin-top: 5px
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