<template>
  <button class="button"
          :class="{ active: isActive }"
          @click="handleClick">
    {{ button.icon }}
  </button>
</template>

<script>
export default {
  name: 'ToolbarButton',
  props: {
    button: {
      type: Object,
      required: true,
    },
    isActive: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    handleClick() {
      this.$emit('click', this.button);
    },
  },
};
</script>

<style scoped>
  .button {
    background: none;
    border: none;
    color: white;
    font-size: 1.2em;
    padding: 10px;
    cursor: pointer;
    transition: background 0.3s ease;
  }

    .button:hover {
      background: rgba(255, 255, 255, 0.2);
    }

    .button.active {
      background: rgba(255, 255, 255, 0.3);
    }
</style>
