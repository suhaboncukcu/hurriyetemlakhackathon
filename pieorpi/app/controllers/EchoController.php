<?php

class EchoController extends ControllerBase
{

    public function indexAction()
    {
    	$this->_debug($_POST);


    	$this->firebase->set('/debug/'.time() , print_r($_POST, true));
    	die();
    }

    public function testAction() {
    	$res = $this->firebase->get( $this->config->firebase->DEFAULT_PATH.'/hurriyetemlakhackathon/addresses');
    	$res = json_decode($res, true);
    	foreach ($res as $re) {
    		$price = $re['price'] / 50;
    		if($price < 5) {
    			$price = 5;
    		}
    		$this->firebase->set( $this->config->firebase->DEFAULT_PATH.'/hurriyetemlakhackathon/addresses/'.$re['unique'].'/price', $price);
    	}
    }	

}

