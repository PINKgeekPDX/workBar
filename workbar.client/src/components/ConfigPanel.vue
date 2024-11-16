<template>
  <div class="config-panel">
    <h2>Toolbar Settings</h2>
    <!-- Add settings options here -->
    <button @click="closePanel">Close</button>
  </div>
</template>

<script>
export default {
  name: 'ConfigPanel',
  methods: {
    closePanel() {
      this.$emit('close');
    },
  },
};
</script>

<style scoped>
  .config-panel {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.5);
    padding: 20px;
    color: #ecf0f1;
  }
</style>
