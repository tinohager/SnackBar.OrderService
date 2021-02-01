<template>
  <span>
    <q-banner
      v-if="showWarning"
      inline-actions
      :class="bannerClass"
    >
      You have lost connection to the server. This app is offline.
      <template v-slot:action>
        {{ connection.connectionState }}
      </template>
    </q-banner>
  </span>
</template>

<script>
export default {
  name: 'ConnectionStatusBar',
  props: {
    connection: {
      type: Object,
      required: true
    }
  },
  computed: {
    showWarning () {
      if (this.connection === undefined) {
        return false
      }

      if (this.connection.connectionState === 'Disconnected') {
        return true
      }

      if (this.connection.connectionState === 'Reconnecting') {
        return true
      }

      return false
    },
    bannerClass () {
      if (this.connection === undefined) {
        return ''
      }

      if (this.connection.connectionState === 'Disconnected') {
        return 'text-white bg-red'
      }

      if (this.connection.connectionState === 'Reconnecting') {
        return 'text-black bg-orange-4'
      }

      return ''
    }
  }
}
</script>
