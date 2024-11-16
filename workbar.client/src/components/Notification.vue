<template>
  <div class="notification" :class="notification.type">
    {{ notification.message }}
  </div>
</template>

<script>
  export default {
    props: ['notification'],
  };
</script>

<style scoped>
  .notification {
    background-color: #34495e;
    color: #ecf0f1;
    padding: 15px;
    border-radius: 5px;
    margin-top: 10px;
    animation: fadeInOut 5s ease-in-out;
  }

    .notification.success {
      background-color: #27ae60;
    }

    .notification.error {
      background-color: #e74c3c;
    }

    .notification.warning {
      background-color: #f1c40f;
    }

  @keyframes fadeInOut {
    0% {
      opacity: 0;
    }

    10%, 90% {
      opacity: 1;
    }

    100% {
      opacity: 0;
    }
  }
</style>
