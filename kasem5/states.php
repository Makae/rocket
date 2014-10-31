<?php
  class State {
    function enterState() {

    }

    function processState() {

    }

    function exitState() {

    }

    function nextState(context) {

    }

  }

  class CompositeState extends State {
    private $currentState = null;

    function enterState() {
      $currentState->enterState();
    }

    function processState() {
      $currentState->processState();
    }

    function exitState() {
      $currentState->exitState();
    }

    function nextState($context) {
      $currentState->nextState($this);
    }

  }
?>