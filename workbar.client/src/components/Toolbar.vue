<template>
  <div class="toolbar"
       :class="{ vertical: isVertical, expanded: isExpanded }"
       :style="{ left: toolbarLeft + 'px', top: toolbarTop + 'px' }"
       @mousedown="startDragging">
    <!-- Edge indicators -->
    <div class="edge-indicator top" :class="{ active: isEdgeIndicatorActive.top }"></div>
    <div class="edge-indicator left" :class="{ active: isEdgeIndicatorActive.left }"></div>
    <div class="edge-indicator right" :class="{ active: isEdgeIndicatorActive.right }"></div>

    <div class="buttons-container">
      <div class="main-buttons">
        <button v-for="button in mainButtons"
                :key="button.id"
                class="button customizable"
                :class="{ active: button.active }"
                :data-button-id="button.id"
                :data-icon="button.icon"
                v-html="button.icon"
                @click="handleButtonClick(button.id)"></button>
      </div>
      <div class="separator"></div>
      <button class="button pin"
              :class="{ active: isPinned }"
              @click="togglePin"
              v-html="isPinned ? '📍' : '📌'"></button>
      <button class="button"
              @click="toggleExpansion"
              v-html="isVertical ? (isExpanded ? '◄' : '►') : (isExpanded ? '▲' : '▼')"></button>
      <div class="expanded-section" v-show="isExpanded">
        <button v-for="button in expandedButtons"
                :key="button.id"
                class="button customizable"
                :class="{ active: button.active }"
                :data-button-id="button.id"
                :data-icon="button.icon"
                v-html="button.icon"
                @click="handleButtonClick(button.id)"></button>
        <button class="button" @click="openCustomizationModal">⚙️</button>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'Toolbar',
    data() {
      return {
        isPinned: false,
        isExpanded: false,
        isVertical: false,
        toolbarLeft: 0,
        toolbarTop: 0,
        isDragging: false,
        startX: 0,
        startY: 0,
        currentPosition: 'top',
        EDGE_THRESHOLD: 80,
        isEdgeIndicatorActive: {
          top: false,
          left: false,
          right: false,
        },
        mainButtons: [
          { id: 'btn1', icon: '🏠', active: false },
          { id: 'btn2', icon: '🔍', active: false },
          { id: 'btn3', icon: '📁', active: false },
          { id: 'btn4', icon: '✏️', active: false },
          { id: 'btn5', icon: '🗑️', active: false },
          { id: 'btn6', icon: '🔧', active: false },
          { id: 'btn7', icon: '📊', active: false },
          { id: 'btn8', icon: '📈', active: false },
          { id: 'btn9', icon: '📅', active: false },
          { id: 'btn10', icon: '⚡', active: false },
        ],
        expandedButtons: [
          { id: 'btn11', icon: '🔒', active: false },
          { id: 'btn12', icon: '🛠️', active: false },
          { id: 'btn13', icon: '🖥️', active: false },
          { id: 'btn14', icon: '🔄', active: false },
          { id: 'btn15', icon: '📦', active: false },
          { id: 'btn16', icon: '📬', active: false },
        ],
      };
    },
    mounted() {
      this.loadCustomIcons();
      document.addEventListener('mousemove', this.handleDragging);
      document.addEventListener('mouseup', this.stopDragging);
    },
    beforeDestroy() {
      document.removeEventListener('mousemove', this.handleDragging);
      document.removeEventListener('mouseup', this.stopDragging);
    },
    methods: {
      loadCustomIcons() {
        const customIcons = JSON.parse(localStorage.getItem('customToolbarIcons')) || {};
        this.mainButtons.forEach(button => {
          if (customIcons[button.id]) {
            button.icon = customIcons[button.id];
          }
        });
        this.expandedButtons.forEach(button => {
          if (customIcons[button.id]) {
            button.icon = customIcons[button.id];
          }
        });
      },
      handleButtonClick(buttonId) {
        // Handle button click event
        console.log(`Button ${buttonId} clicked`);
      },
      togglePin() {
        this.isPinned = !this.isPinned;
      },
      toggleExpansion() {
        this.isExpanded = !this.isExpanded;
      },
      openCustomizationModal() {
        this.$emit('open-customization-modal');
      },
      startDragging(event) {
        if (this.isPinned || event.target.classList.contains('button')) return;
        this.isDragging = true;
        this.startX = event.clientX - this.toolbarLeft;
        this.startY = event.clientY - this.toolbarTop;
        this.$el.style.transition = 'none';
        document.body.classList.add('no-select');
      },
      handleDragging(event) {
        if (!this.isDragging) return;
        let newLeft = event.clientX - this.startX;
        let newTop = event.clientY - this.startY;
        const edge = this.getClosestEdge(newLeft, newTop);
        this.updateEdgeIndicator(edge);
        if (edge === 'top') {
          newTop = 0;
          newLeft = Math.max(0, Math.min(newLeft, window.innerWidth - this.$el.offsetWidth));
        } else if (edge === 'left') {
          newLeft = 0;
          newTop = Math.max(0, Math.min(newTop, window.innerHeight - this.$el.offsetHeight));
        } else if (edge === 'right') {
          newLeft = window.innerWidth - this.$el.offsetWidth;
          newTop = Math.max(0, Math.min(newTop, window.innerHeight - this.$el.offsetHeight));
        }
        this.toolbarLeft = newLeft;
        this.toolbarTop = newTop;
        this.updateToolbarOrientation(edge);
      },
      stopDragging() {
        if (!this.isDragging) return;
        this.isDragging = false;
        this.$el.style.transition = 'all 0.3s ease';
        this.hideEdgeIndicators();
        document.body.classList.remove('no-select');
      },
      getClosestEdge(left, top) {
        if (top < this.EDGE_THRESHOLD) return 'top';
        if (left < this.EDGE_THRESHOLD) return 'left';
        if (window.innerWidth - left - this.$el.offsetWidth < this.EDGE_THRESHOLD) return 'right';
        return this.currentPosition;
      },
      updateEdgeIndicator(edge) {
        this.isEdgeIndicatorActive.top = edge === 'top';
        this.isEdgeIndicatorActive.left = edge === 'left';
        this.isEdgeIndicatorActive.right = edge === 'right';
      },
      hideEdgeIndicators() {
        this.isEdgeIndicatorActive.top = false;
        this.isEdgeIndicatorActive.left = false;
        this.isEdgeIndicatorActive.right = false;
      },
      updateToolbarOrientation(position) {
        if (this.currentPosition === position) return;
        this.currentPosition = position;
        this.isVertical = position === 'left' || position === 'right';
      },
    },
  };
</script>

<style scoped>
  .toolbar {
    position: fixed;
    background: rgba(255, 255, 255, 0.1);
    backdrop-filter: blur(10px);
    border: 1px solid rgba(255, 255, 255, 0.2);
    border-radius: 10px;
    box-shadow: 0 8px 32px rgba(0, 0, 0, 0.5);
    padding: 6px;
    z-index: 1000;
    transition: all 0.3s ease;
    display: flex;
    flex-direction: row;
    align-items: center;
    user-select: none;
  }

    .toolbar.vertical {
      flex-direction: column;
    }

  .buttons-container {
    display: flex;
    gap: 6px;
  }

  .toolbar.vertical .buttons-container {
    flex-direction: column;
  }

  .main-buttons {
    display: flex;
    gap: 6px;
  }

  .toolbar.vertical .main-buttons {
    flex-direction: column;
  }

  .button {
    width: 32px;
    height: 32px;
    border: none;
    border-radius: 6px;
    background: rgba(255, 255, 255, 0.2);
    border: 1px solid rgba(255, 255, 255, 0.3);
    color: #ffffff;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 14px;
    transition: all 0.3s ease, transform 0.2s ease;
    position: relative;
    overflow: hidden;
    user-select: none;
  }

    .button::before {
      content: '';
      position: absolute;
      top: 50%;
      left: 50%;
      width: 300%;
      height: 300%;
      background: rgba(255, 255, 255, 0.2);
      transition: all 0.5s ease;
      transform: translate(-50%, -50%) scale(0);
      border-radius: 50%;
    }

    .button:hover::before {
      transform: translate(-50%, -50%) scale(1);
    }

    .button:hover {
      background: rgba(255, 255, 255, 0.3);
      transform: scale(1.05);
    }

    .button.active {
      background: rgba(99, 102, 241, 0.6);
      border-color: rgba(99, 102, 241, 0.8);
      color: #ffffff;
      box-shadow: 0 4px 12px rgba(99, 102, 241, 0.6);
    }

  .expanded-section {
    display: none;
    gap: 6px;
    margin-left: 6px;
  }

  .toolbar.expanded .expanded-section {
    display: flex;
  }

  .toolbar.vertical .expanded-section {
    flex-direction: column;
    margin-left: 0;
    margin-top: 6px;
  }

  .separator {
    width: 2px;
    height: 32px;
    background: rgba(255, 255, 255, 0.2);
    margin: 0 6px;
    transition: all 0.3s ease;
  }

  .toolbar.vertical .separator {
    width: 32px;
    height: 2px;
    margin: 6px 0;
  }

  /* Edge indicators */
  .edge-indicator {
    position: fixed;
    pointer-events: none;
    background: rgba(99, 102, 241, 0.4);
    z-index: 999;
    opacity: 0;
    transition: opacity 0.3s ease, transform 0.3s ease;
  }

    .edge-indicator.active {
      opacity: 1;
      transform: scale(1.1);
    }

    .edge-indicator.top {
      top: 0;
      left: 0;
      right: 0;
      height: 6px;
      border-radius: 0 0 12px 12px;
    }

    .edge-indicator.left {
      top: 0;
      left: 0;
      bottom: 0;
      width: 6px;
      border-radius: 0 12px 12px 0;
    }

    .edge-indicator.right {
      top: 0;
      right: 0;
      bottom: 0;
      width: 6px;
      border-radius: 12px 0 0 12px;
    }
</style>
