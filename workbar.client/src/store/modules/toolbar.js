// src/store/modules/toolbar.js
import { defineStore } from 'pinia';
import api from '@/services/api';

export const useToolbarStore = defineStore('toolbar', {
  state: () => ({
    buttons: [
      // Default button configurations
      {
        id: 'btn1',
        icon: '🏠',
        customizable: true,
        actionType: 'url',
        actionConfig: {
          url: 'https://example.com',
        },
      },
      // Add more buttons as needed
    ],
    notifications: [],
  }),
  actions: {
    executeAction(button) {
      switch (button.actionType) {
        case 'url':
          window.open(button.actionConfig.url, '_blank');
          break;
        case 'app':
          this.launchApplication(button.actionConfig);
          break;
        case 'script':
          this.executeScript(button.actionConfig);
          break;
        case 'macro':
          this.playMacro(button.actionConfig);
          break;
        default:
          console.warn('Unknown action type:', button.actionType);
      }
    },
    launchApplication(config) {
      api.launchApp(config)
        .then(() => {
          this.addNotification('Application launched successfully.', 'success');
        })
        .catch((error) => {
          console.error(error);
          this.addNotification('Failed to launch application.', 'error');
        });
    },
    executeScript(config) {
      api.executeScript(config)
        .then(() => {
          this.addNotification('Script executed successfully.', 'success');
        })
        .catch((error) => {
          console.error(error);
          this.addNotification('Failed to execute script.', 'error');
        });
    },
    playMacro(config) {
      // Implement macro playback if possible
      this.addNotification('Macro playback is not implemented.', 'warning');
    },
    updateButtons(updatedButtons) {
      this.buttons = updatedButtons;
      // Optionally, save to backend or local storage
    },
    addNotification(message, type) {
      const id = Date.now();
      this.notifications.push({ id, message, type });
      setTimeout(() => {
        this.notifications = this.notifications.filter(n => n.id !== id);
      }, 5000); // Remove after 5 seconds
    },
  },
});
