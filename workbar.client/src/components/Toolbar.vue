<template>
  <div ref="toolbar"
       :class="['toolbar', { vertical: isVertical, expanded: isExpanded, pinned: isPinned }]"
       :style="toolbarStyle"
       @mousedown="startDrag">
    <div class="buttons-container">
      <!-- Main Buttons -->
      <div class="main-buttons">
        <button v-for="(button, index) in visibleButtons"
                :key="button.id"
                class="button customizable"
                @click="executeAction(button)">
          {{ button.icon }}
        </button>
      </div>

      <!-- Control Buttons -->
      <div class="control-buttons">
        <button class="button" @click="togglePin">{{ isPinned ? '📍' : '📌' }}</button>
        <button class="button" @click="toggleExpand">{{ expandIcon }}</button>
        <button class="button" @click="openCustomizationModal">⚙️</button>
        <button class="button" @click="openThemeSelector">🎨</button>
        <button class="button" @click="openAboutDialog">ℹ️</button>
      </div>
    </div>

    <!-- Modals -->
    <CustomizationModal v-if="showCustomizationModal"
                        :buttons="buttons"
                        @close="closeCustomizationModal"
                        @save="saveButtonConfigurations" />
    <ThemeSelector v-if="showThemeSelector" @close="closeThemeSelector" />
    <AboutDialog v-if="showAboutDialog" @close="closeAboutDialog" />
  </div>

  <!-- Notifications -->
  <div class="notifications">
    <Notification v-for="notification in notifications"
                  :key="notification.id"
                  :notification="notification" />
  </div>
</template>

<script>
  import { ref, computed, onMounted, onBeforeUnmount } from 'vue';
  import { useToolbarStore } from '@/store/toolbar';
  import CustomizationModal from './CustomizationModal.vue';
  import ThemeSelector from './ThemeSelector.vue';
  import AboutDialog from './AboutDialog.vue';
  import Notification from './Notification.vue';

  export default {
    components: {
      CustomizationModal,
      ThemeSelector,
      AboutDialog,
      Notification,
    },
    setup() {
      const toolbarStore = useToolbarStore();

      const toolbar = ref(null);
      const isDragging = ref(false);
      const startX = ref(0);
      const startY = ref(0);
      const position = ref({ x: 0, y: 0 });
      const activeEdge = ref('top');
      const isPinned = ref(false);
      const isExpanded = ref(false);
      const showCustomizationModal = ref(false);
      const showThemeSelector = ref(false);
      const showAboutDialog = ref(false);

      const buttons = ref(toolbarStore.buttons);
      const notifications = computed(() => toolbarStore.notifications);

      const visibleButtons = computed(() => {
        return isExpanded.value ? buttons.value : buttons.value.slice(0, 10);
      });

      const isVertical = computed(() => ['left', 'right'].includes(activeEdge.value));
      const expandIcon = computed(() => (isVertical.value ? (isExpanded.value ? '◄' : '►') : isExpanded.value ? '▲' : '▼'));

      const toolbarStyle = computed(() => ({
        left: `${position.value.x}px`,
        top: `${position.value.y}px`,
      }));

      // Draggable functionality
      const startDrag = (e) => {
        if (isPinned.value || e.target.classList.contains('button')) return;

        isDragging.value = true;
        const rect = toolbar.value.getBoundingClientRect();
        startX.value = e.clientX - rect.left;
        startY.value = e.clientY - rect.top;

        document.addEventListener('mousemove', handleDrag);
        document.addEventListener('mouseup', stopDrag);
      };

      const handleDrag = (e) => {
        if (!isDragging.value) return;

        let x = e.clientX - startX.value;
        let y = e.clientY - startY.value;

        // Snap to edges
        const edgeThreshold = 80;
        const rect = toolbar.value.getBoundingClientRect();
        if (y < edgeThreshold) activeEdge.value = 'top';
        else if (x < edgeThreshold) activeEdge.value = 'left';
        else if (window.innerWidth - x - rect.width < edgeThreshold) activeEdge.value = 'right';

        updatePosition(x, y);
      };

      const stopDrag = () => {
        isDragging.value = false;
        document.removeEventListener('mousemove', handleDrag);
        document.removeEventListener('mouseup', stopDrag);
      };

      const updatePosition = (x, y) => {
        const rect = toolbar.value.getBoundingClientRect();
        switch (activeEdge.value) {
          case 'top':
            position.value.x = Math.max(0, Math.min(x, window.innerWidth - rect.width));
            position.value.y = 0;
            break;
          case 'left':
            position.value.x = 0;
            position.value.y = Math.max(0, Math.min(y, window.innerHeight - rect.height));
            break;
          case 'right':
            position.value.x = window.innerWidth - rect.width;
            position.value.y = Math.max(0, Math.min(y, window.innerHeight - rect.height));
            break;
          default:
            break;
        }
      };

      // Button actions
      const executeAction = (button) => {
        toolbarStore.executeAction(button);
      };

      const togglePin = () => {
        isPinned.value = !isPinned.value;
      };

      const toggleExpand = () => {
        isExpanded.value = !isExpanded.value;
      };

      const openCustomizationModal = () => {
        showCustomizationModal.value = true;
      };

      const closeCustomizationModal = () => {
        showCustomizationModal.value = false;
      };

      const saveButtonConfigurations = (updatedButtons) => {
        buttons.value = updatedButtons;
        toolbarStore.updateButtons(updatedButtons);
      };

      const openThemeSelector = () => {
        showThemeSelector.value = true;
      };

      const closeThemeSelector = () => {
        showThemeSelector.value = false;
      };

      const openAboutDialog = () => {
        showAboutDialog.value = true;
      };

      const closeAboutDialog = () => {
        showAboutDialog.value = false;
      };

      // Auto-collapse functionality
      let autoCollapseTimer = null;
      const autoCollapseTime = 2 * 60 * 1000; // 2 minutes

      const resetAutoCollapseTimer = () => {
        clearTimeout(autoCollapseTimer);
        if (!isPinned.value && isExpanded.value) {
          autoCollapseTimer = setTimeout(() => {
            isExpanded.value = false;
          }, autoCollapseTime);
        }
      };

      // Event listeners for user activity
      const userActivityHandler = () => {
        resetAutoCollapseTimer();
      };

      onMounted(() => {
        document.addEventListener('mousemove', userActivityHandler);
        document.addEventListener('mousedown', userActivityHandler);
        resetAutoCollapseTimer();
      });

      onBeforeUnmount(() => {
        document.removeEventListener('mousemove', userActivityHandler);
        document.removeEventListener('mousedown', userActivityHandler);
        clearTimeout(autoCollapseTimer);
      });

      return {
        toolbar,
        isPinned,
        isExpanded,
        startDrag,
        isVertical,
        expandIcon,
        toolbarStyle,
        visibleButtons,
        executeAction,
        togglePin,
        toggleExpand,
        openCustomizationModal,
        closeCustomizationModal,
        saveButtonConfigurations,
        openThemeSelector,
        closeThemeSelector,
        openAboutDialog,
        closeAboutDialog,
        showCustomizationModal,
        showThemeSelector,
        showAboutDialog,
        notifications,
      };
    },
  };
</script>

<style scoped>
  .toolbar {
    position: absolute;
    display: flex;
    background: rgba(255, 255, 255, 0.1);
    backdrop-filter: blur(10px);
    border-radius: 10px;
    padding: 5px;
    user-select: none;
    cursor: move;
  }

    .toolbar.vertical {
      flex-direction: column;
    }

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

  .buttons-container {
    display: flex;
  }

  .control-buttons {
    display: flex;
  }

  .separator {
    width: 1px;
    background: rgba(255, 255, 255, 0.2);
    margin: 0 5px;
  }

  .notifications {
    position: fixed;
    bottom: 20px;
    right: 20px;
  }

  .notification {
    margin-bottom: 10px;
  }
</style>
