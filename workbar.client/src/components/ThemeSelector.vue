<template>
  <div class="modal" @click.self="closeModal">
    <div class="modal-content">
      <div class="modal-header">
        <h2>Select Theme</h2>
        <span class="close" @click="closeModal">&times;</span>
      </div>
      <div class="modal-body">
        <div class="themes-list">
          <div v-for="theme in themes"
               :key="theme.name"
               class="theme-item"
               @click="applyTheme(theme.name)">
            <img :src="theme.preview" :alt="theme.name" />
            <p>{{ theme.displayName }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import themesData from '@/themes/themes.json';

export default {
  data() {
    return {
      themes: themesData,
    };
  },
  methods: {
    closeModal() {
      this.$emit('close');
    },
    applyTheme(themeName) {
      const themePath = `/themes/${themeName}/theme.css`;
      let linkElement = document.getElementById('themeStylesheet');

      if (linkElement) {
        linkElement.href = themePath;
      } else {
        linkElement = document.createElement('link');
        linkElement.id = 'themeStylesheet';
        linkElement.rel = 'stylesheet';
        linkElement.href = themePath;
        document.head.appendChild(linkElement);
      }

      localStorage.setItem('selectedTheme', themeName);
      this.closeModal();
    },
  },
  mounted() {
    const savedTheme = localStorage.getItem('selectedTheme');
    if (savedTheme) {
      this.applyTheme(savedTheme);
    }
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
    max-width: 800px;
    border-radius: 8px;
    color: #ecf0f1;
  }

  .modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .themes-list {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
    margin-top: 20px;
  }

  .theme-item {
    width: calc(33% - 20px);
    cursor: pointer;
    text-align: center;
  }

    .theme-item img {
      width: 100%;
      border-radius: 4px;
    }

    .theme-item p {
      margin-top: 10px;
    }
</style>
