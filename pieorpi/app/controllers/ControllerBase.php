<?php

use Phalcon\Mvc\Controller;

class ControllerBase extends Controller
{
	public function _debug($message) {
        echo "<pre>";
        var_dump($message);
        echo "</pre>";

        return;
    }
}
