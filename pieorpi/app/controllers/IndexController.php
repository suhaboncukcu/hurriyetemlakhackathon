<?php

class IndexController extends ControllerBase
{

    public function indexAction()
    {
    	$hmm = $this->config->firebase->DEFAULT_PATH.'/hurriyetemlakhackathon';
    	$k = $this->firebase->get($hmm);
    	$tmo = json_decode($k, true);
    	echo sizeof($tmo['addresses']);
		die();
    }

}

