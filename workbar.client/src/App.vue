<template>
  <div id="app">
    <Toolbar />
  </div>
</template>

<script>
  import Toolbar from './components/Toolbar.vue';

  export default {
    name: 'App',
    components: {
      Toolbar,
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
  }
</style>
