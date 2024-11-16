<template>
  <div class="modal" @click.self="closeModal">
    <div class="modal-content">
      <div class="modal-header">
        <h2>About WorkBar</h2>
        <span class="close" @click="closeModal">&times;</span>
      </div>
      <div class="modal-body">
        <p>Version: {{ version }}</p>
        <p>Developed by: Your Name</p>
        <p>
          <a href="https://github.com/yourusername/workbar" target="_blank">GitHub Repository</a>
        </p>
        <p>
          <button @click="checkForUpdates">Check for Updates</button>
        </p>
        <p v-if="updateStatus">{{ updateStatus }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      version: '1.0.0',
      updateStatus: '',
    };
  },
  methods: {
    closeModal() {
      this.$emit('close');
    },
    async checkForUpdates() {
      try {
        const response = await axios.get('https://api.github.com/repos/yourusername/workbar/releases/latest');
        const latestVersion = response.data.tag_name;
        if (latestVersion !== this.version) {
          this.updateStatus = `A new version (${latestVersion}) is available. Please update your application.`;
        } else {
          this.updateStatus = 'You are using the latest version.';
        }
      } catch (error) {
        console.error(error);
        this.updateStatus = 'Failed to check for updates.';
      }
    },
  },
};
</script>

<style scoped>
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0,0,0,0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background-color: #2c3e50;
    padding: 20px;
    width: 80%;
    max-width: 500px;
    border-radius: 8px;
    color: #ecf0f1;
  }

  .modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .close {
    cursor: pointer;
    font-size: 1.5em;
  }

  .modal-body {
    margin-top: 10px;
  }

    .modal-body p {
      margin-bottom: 15px;
    }

    .modal-body a {
      color: #3498db;
      text-decoration: none;
    }

    .modal-body button {
      padding: 10px 20px;
      background-color: #27ae60;
      border: none;
      border-radius: 4px;
      color: white;
      cursor: pointer;
    }

      .modal-body button:hover {
        background-color: #2ecc71;
      }
</style>
