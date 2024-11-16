<template>
  <div id="app">
    <Toolbar />
    <CustomizationModal v-if="showCustomizationModal" @close="closeCustomizationModal" />
  </div>
</template>

<script>
  import Toolbar from './components/Toolbar.vue';
  import CustomizationModal from './components/CustomizationModal.vue';

  export default {
    name: 'App',
    components: {
      Toolbar,
      CustomizationModal,
    },
    data() {
      return {
        showCustomizationModal: false,
      };
    },
    methods: {
      closeCustomizationModal() {
        this.showCustomizationModal = false;
      },
    },
  };
</script>

<style>
  body {
    margin: 0;
    padding: 0;
    background: linear-gradient(135deg, #1e2327, #2c3e50);
    height: 100vh;
    overflow: hidden;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    display: flex;
    justify-content: center;
    align-items: center;
    transition: background 0.3s ease;
  }
</style>
