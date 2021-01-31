<template>
  <q-layout view="lHh Lpr lFf">
    <q-header
      elevated
      class="text-white"
      style="background: #24292e"
      height-hint="61.59"
    >
      <q-toolbar class="q-py-sm q-px-md">
        <q-btn
          dense
          flat
          icon="fastfood"
          to="/"
          size="19px"
          color="white"
          class="q-mr-sm"
          label="SnackBar.OrderService"
        />

        <div
          class="Menu__toolbar-link q-ml-xs q-gutter-md text-body2 text-weight-bold row items-center no-wrap"
        >
          <router-link to="/cashpoint">
            Cash Point
          </router-link>
          <router-link to="/kitchen">
            Kitchen
          </router-link>
        </div>
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
export default {
  name: 'MainLayout',
  data () {
    return {

    }
  }
}
</script>

<style lang="sass">
.Menu
  &__toolbar-link
    a
      color: white
      text-decoration: none
      &:hover
        opacity: 0.7
</style>
