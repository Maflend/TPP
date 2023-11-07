<template>
  <div class="modal" id="modal">
    <div class="container">
      <div class="contact-info">
        <img class="contact-info-img" :src="object.avatarUrl" alt="" />
        <div class="contact-info-state">
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
          <div class="contact-info-state-name">{{ object.displayName }}</div>
        </div>
      </div>
      <div class="ailling-fields">
        <div class="ailling-fields-text">
          <p
            class="ailling-fields-text-posts"
            v-for="post in object.posts"
            :class="{ active: post.isPositive, disconnect: !post.isPositive }"
          >
            {{ post.text }}
          </p>
        </div>
        <div class="ailling-fields_comments">
          <textarea
            v-model="postText"
            class="ailling-fields_comments-textarea"
            name="comment"
            id="comment"
            cols="40"
            rows="5"
          ></textarea>
          <div class="ailling-fields_comments-buttons">
            <button
              class="ailling-fields_comments-buttons-btn-good style-btn"
              @click="createPost(true)"
            >
              <!-- <img class="" src="/src/assets/like.svg" alt=""> -->
              Хорошо
            </button>
            <button
              class="ailling-fields_comments-buttons-btn-nogood style-btn"
              @click="createPost(false)"
            >
              Плохой
            </button>
          </div>
        </div>
      </div>
      <button @click.prevent="closefn" class="close">
        <img src="../assets/close1.svg" alt="" />
      </button>
    </div>
  </div>
</template>
    
<script>
import axios from "axios";
import { API_BEC_DIS } from "../api";

export default {
  props: {
    id: String,
    closefn: Function,
  },

  mounted() {
    axios
      .get(`${API_BEC_DIS}users/` + this.id + "/posts", {
        headers: {
          "ngrok-skip-browser-warning": "true",
        },
      })
      .then((res) => {
        this.object = res.data;
      });
  },

  data() {
    return {
      object: {},
      postText: "",
    };
  },

  methods: {
    Close() {
      this.$emit("toggle-modal");
    },
    createPost(isPositive) {
      if (this.postText == "") {
        window.alert("Далбакряк");
        return;
      }
      axios({
        method: "post",
        url: `${API_BEC_DIS}posts`,
        headers: {
          "ngrok-skip-browser-warning": "true",
        },
        data: {
          userId: this.object.id,
          text: this.postText,
          isPositive: isPositive,
        },
      }).then((res) => {
        this.postText = "";
        axios
          .get(`${API_BEC_DIS}users/` + this.id + "/posts", {
            headers: {
              "ngrok-skip-browser-warning": "true",
            },
          })
          .then((res) => {
            this.object = res.data;
          });
      });
    },
  },
};
</script>


<style lang="sass" scoped>
.active
  border: 2px solid green
.disconnect
  border: 2px solid red
.style-btn
  background: #d99f5f
  padding: 10px 20px
  margin-left: 20px
  border-radius: 15px
.modal
  background-color: rgba(0, 0, 0, 0.7)
  position: absolute
  width: 100%
  height: 100%
  left: 0
  top: 0
  overflow: auto
  display: flex
  align-items: center
  justify-content: center
  & .container
    position: relative
    border-radius: 10px
    padding: 100px 40px 40px 40px
    background: var(--cart)
    display: flex
    & .close
      position: absolute
      top: 20px
      right: 20px
      width: 40px
      height: 40px
      border: 0
      outline: 1px solid #000
      background: 0
      &:hover
        outline: 2px solid #000
    & .contact-info
      margin-right: 100px
      &-img
        width: 250px
        border-radius: 15px
        margin: 0 auto
        margin-bottom: 30px
      &-state
        display: flex
        flex-direction: column
        &-name
          font-size: 30px
        & .online
          display: flex
          align-items: center
        & .cart-state_red-circle
          width: 10px
          height: 10px
          background: red
          border-radius: 100%
          margin-left: 10px
          margin-right: 5px
        & .cart-state_green-circle
          width: 10px
          height: 10px
          background: green
          border-radius: 100%
          margin-left: 10px
          margin-right: 5px
    & .ailling-fields
      &-text
        padding-top: 20px
        height: 400px
        background: #996ca5
        border-radius: 15px
        border: 2px solid #000
        margin-bottom: 30px
        overflow: auto
        &-posts
          padding: 20px
          margin-bottom: 20px
          margin-left: 20px
          border-radius: 15px
          width: 350px
      &_comments
        display: flex
        &-textarea
          background: #ec6d6d
          border-radius: 15px
          border: 1px solid #000
          outline: none
        &-buttons
          display: flex
          flex-direction: column
          justify-content: space-between
          &-btn-good
            color: #34D800
            outline: 1px solid #34D800
            border: 0
            &:hover
              outline: 2px solid #34D800
          &-btn-nogood
            color: #FF0000
            outline: 1px solid #FF0000
            border: 0
            &:hover
              outline: 2px solid #FF0000

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
  border-radius: 65px
*::-webkit-scrollbar-thumb,
html *::-webkit-scrollbar-thumb
  background: #d99f5f
  border-radius: 5px
  border: 3px solid #d99f5f
</style>

