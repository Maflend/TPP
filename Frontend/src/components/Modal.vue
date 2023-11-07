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
          <div class="contact-info-state-likes">
            <button class="contact-info-state-likes_like style">
              <img class="" src="/src/assets/like.svg" alt="" />{{
                object.totalPositiveCount
              }}
            </button>
            <button class="contact-info-state-likes_dislike style">
              <img class="" src="/src/assets/dislike.svg" alt="" />{{
                object.totalNegativeCount
              }}
            </button>
          </div>
        </div>
      </div>
      <div class="ailling-fields">
        <div class="ailling-fields-messages">ОТЗЫВЫ ({{object.totalPositiveCount + object.totalNegativeCount}})</div>
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
            placeholder="Оставить отзыв..."
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
              Хорошо
              <img src="/src/assets/like.svg" alt="">
            </button>
            <button
              class="ailling-fields_comments-buttons-btn-nogood style-btn"
              @click="createPost(false)"
            >
              Плохой
              <img src="/src/assets/dislike.svg" alt="">
            </button>
          </div>
        </div>
      </div>
      <button @click.prevent="closefn" class="close">
        <img class="close-img" src="../assets/arrowleft.svg" alt="" />
        <p class="close-text">Назад</p>
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

.style
  border: 0
  border-radius: 10px
  background-color: rgba(255,0,0,0)
.active
  border: 2px solid green
.disconnect
  border: 2px solid red
.style-btn
  background: #fff
  padding: 10px 20px
  border-radius: 10px
  width: 180px
  margin-top: 20px
  display: flex
  align-items: center
  justify-content: center
  & img
    width: 20px
    margin-left: 13px
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
      left: 20px
      border: 0
      background: 0
      display: flex
      align-items: center
      cursor: pointer
      &-img
        width: 25px
        margin-right: 0
      &-text
        font-size: 26px
        padding-left: 10px
        color: #4b4b4b
        &:hover
          color: #000
    & .contact-info
      margin-right: 50px
      &-img
        width: 250px
        border-radius: 15px
        margin: 0 auto
        &:hover
          width: 300px
      &-state
        display: flex
        flex-direction: column
        &-likes
          display: flex
          margin-top: 15px
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
        &-name
          font-size: 30px
          color: #4b4b4b
          margin-top: 10px
        & .online
          display: flex
          align-items: center
        & .cart-state_red-circle
          width: 10px
          height: 10px
          background: red
          border-radius: 100%
          margin-right: 5px
        & .cart-state_green-circle
          width: 10px
          height: 10px
          background: green
          border-radius: 100%
          margin-left: 10px
          margin-right: 5px
    & .ailling-fields
      &-messages
        height: 50px
        border: 1px solid #e8e8e8
        border-bottom: 0
        border-radius: 10px 10px 0 0 
        background: #f6f8f9
        color: #4b4b4b
        display: flex
        align-items: center
        padding-left: 20px
      &-text
        padding-top: 20px
        height: 320px
        border: 1px solid #e8e8e8
        overflow: auto
        &-posts
          padding: 20px
          margin-bottom: 20px
          margin-left: 20px
          border-radius: 15px
          width: 350px
      &_comments
        &-textarea
          padding-top: 10px
          padding-left: 20px
          border-radius: 0 0 10px 10px
          border: 1px solid #e8e8e8
          border-top: 0
          outline: none
          height: 50px
        &-buttons
          display: flex
          justify-content: space-between
          &-btn-good
            color: #4b4b4b
            outline: 1px solid #e8e8e8
            border: 0
            transition: 0.3s
            &:hover
              background: #f0f0f1
          &-btn-nogood
            color: #4b4b4b
            outline: 1px solid #e8e8e8
            border: 0
            transition: 0.3s
            &:hover
              background: #f0f0f1

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
