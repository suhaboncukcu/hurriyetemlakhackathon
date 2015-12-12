<?php

class EchoController extends ControllerBase
{

    public function indexAction()
    {
    	$this->_debug($_POST);


    	$this->firebase->set('/debug/'.time() , print_r($_POST, true));
    	die();
    }

}

