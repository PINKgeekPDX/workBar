// src/store/modules/settings.js
import { defineStore } from 'pinia';

export const useSettingsStore = defineStore('settings', {
  state: () => ({
    isPinned: false,
    isExpanded: false,
    position: { x: 0, y: 0 },
    activeEdge: 'top',
    theme: 'default',
  }),
  actions: {
    togglePin() {
      this.isPinned = !this.isPinned;
    },
    toggleExpand() {
      this.isExpanded = !this.isExpanded;
    },
    setPosition(position) {
      this.position = position;
    },
    setActiveEdge(edge) {
      this.activeEdge = edge;
    },
    setTheme(themeName) {
      this.theme = themeName;
    },
  },
});
