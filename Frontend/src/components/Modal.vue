<template>
  <div class="modal" id="modal">
    <div class="container">
      <div class="contact-info">
        <img class="contact-info-img" :src="object.avatarUrl" alt="" />
        <div class="contact-info-state">
          <div class="contact-info-state-name">{{ object.displayName }}</div>
          <div v-if="!object.isOnline" class="contact-info-state_red-circle"></div>
          <div v-else class="contact-info-state_green-circle"></div>
        </div>
      </div>
      <div class="ailling-fields">
        <div class="ailling-fields-text"></div>
        <div class="ailling-fields_comments">
          <textarea class="ailling-fields_comments-textarea" name="comment" id="comment" cols="40" rows="5"></textarea>
          <div class="ailling-fields_comments-buttons">
            <button class="ailling-fields_comments-buttons-btn-good style-btn">Хороший</button>
            <button class="ailling-fields_comments-buttons-btn-nogood style-btn">Плохой</button>
          </div>
        </div>
      </div>
    </div>
    <button @click.prevent="closefn" class="close">Close</button>
  </div>
</template>
    
<script>
import axios from "axios";
export default {
  props: {
    id: String,
    closefn: Function,
  },

  mounted() {
    axios
      .get(
        "https://32f8-95-28-137-40.ngrok-free.app/api/users/" +
          this.id +
          "/posts",
        {
          headers: {
            "ngrok-skip-browser-warning": "true",
          },
        }
      )
      .then((res) => {
        this.object = res.data;
      });
  },

  data() {
    return {
      object: {},
    };
  },

  methods: {
    Close() {
      this.$emit("toggle-modal");
    },
  },
};
</script>
    
    
<style lang="sass" scoped>
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
    border: 2px solid #000
    border-radius: 10px
    padding: 100px
    background: #d99f5f
    display: flex
    & .contact-info
      margin-right: 100px
      &-img
        width: 150px
        border-radius: 100%
        margin: 0 auto
        margin-bottom: 30px
      &-state
        display: flex
        align-items: center
        justify-content: center
        &-name
          font-size: 30px
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
    & .ailling-fields
      &-text
        height: 400px
        background: #996ca5
        border-radius: 15px
        border: 3px solid #000
        margin-bottom: 30px
      &_comments
        display: flex
        &-textarea
          background: #d99f5f
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


  & .close
    display: none
</style>

