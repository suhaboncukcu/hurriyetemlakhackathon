<?php

class ApiController extends ControllerBase {

	var $default_path;
	var $lat_value;
	var $lng_value;

	public function onConstruct() {
    	$key = $this->request->getPost('apikey');
    	$this->default_path = $this->config->firebase->DEFAULT_PATH.'/'.$key.'/';
		


		$this->view->disable();
        $this->response->setContentType('application/json', 'UTF-8');

        $action = $this->dispatcher->getActionName();
        if($action != 'warning') {
        	if(!$key) {
	    		$this->response->redirect('api/warning/003');
	    	}

            /*
	    	if($this->request->getHeader('mobilekey') != $key) {
	    		$this->response->redirect('api/warning/004');
	    	} 
            */

            $provider = $this->firebase->get($this->default_path);
            $provider = json_decode($provider, true);
            if(sizeof($provider) == 0 ) {
                $this->response->redirect('api/warning/009');
            }
        }


        $this->lat_value = 110.574;
		$this->lng_value = 111.320 * cos($this->$lat_value);		
	}

    public function indexAction() {  	
		$this->response->setContent($this->giveWarning('001'));
		return $this->response;
    } 

    public function createAction() {  	
		$this->firebase->set($this->default_path, 'hedehede');
		$this->response->setContent($this->giveWarning('002'));
		return $this->response;
    } 

    //////// API USAGE FUNCTIONS /////////////////////////////////
    /*
    * Since this is a hackathon, everything will be in the same place. 
    * Sorry, not sorry. 
    * Meh. 
    */

    /*
    * Because you know, nobody trusts anyone nowadays.
	*
    * should be a json object:
    * $uniqe = '', 
	* $lat = '', 
	* $lng = '', 
	* $needPhoto = '1', 
	* $declinedPhotoTrialCount = '0',
	* $price = 01,
	* $ownerPhone = ''
    */
    public function newAddressAction() {
    	$property = $this->request->getPost('address');
    	$property = json_decode($property, true);

    	if(!$property['unique'] || $property['unique'] == '') {
    		$this->response->setContent($this->giveWarning('005'));	
    		return $this->response;
    	}

    	$this->firebase->set($this->default_path.'addresses/'.$property['unique'], $property );
    	$this->response->setContent($this->giveWarning('002'));
		return $this->response;
    }

    public function updateAddressAction($id) {
    	$property = $this->request->getPost('address');
    	$property = json_decode($property, true);

    	if(!$property['unique'] || $property['unique'] == '') {
    		$this->response->setContent($this->giveWarning('005'));	
    		return $this->response;
    	}

    	$tmp = array();
    	foreach ($property as $p) {
    		if($p && $p != '') {
    			array_push($tmp, $p);
    		}
    	}

    	$this->firebase->update($this->default_path.'addresses/'.$id, $tmp );
    	$this->response->setContent($this->giveWarning('002'));
		return $this->response;
    }

    public function deleteAddressAction($id) {
    	$this->firebase->delete($this->default_path.'addresses/'.$id );
    	$this->response->setContent($this->giveWarning('002'));
		return $this->response;
    }


    public function searchAction() {
    	$searchObj = $this->request->getPost('search');
    	$search = json_decode($searchObj, true);

    	$lat = $search['lat'];
    	$lng = $search['lng'];
    	$parameter = $search['parameter'];

    	$lat_dif = $parameter / $this->lat_value;
    	$lng_dif = $parameter / $this->lng_value;

    	$max_lat = $lat + $lat_dif;
    	$min_lat = $lat - $lat_dif;

    	$max_lng = $lng + $lng_dif;
    	$min_lng = $lng - $lng_dif;


    	$all = $this->firebase->get($this->default_path.'addresses');
    	$alls = json_decode($all, true);


    	// this is disgusting but.. for now, it works. 

    	$lat_res = array();
    	//first lat
    	foreach ($alls as $p) {
    		if($p['lat'] < $max_lat && $p['lat'] > $min_lat) {
    			array_push($lat_res, $p);
    		}
    	}

    	$lng_res = array();
    	//then lng
    	foreach ($lat_res as $t) {
    		if($t['lng'] < $max_lng && $t['lng'] > $min_lng) {
    			array_push($lng_res, $t);
    		}
    	}

        $lng_res['count'] = sizeof($lng_res);
    	$this->response->setContent(json_encode($lng_res));
		return $this->response;

    }

    public function newPhotographerAction() {
    	$photographer = $this->request->getPost('photographer');
    	$photographer = json_decode($photographer, true);

    	if(!$photographer['email'] || $photographer['email'] == '') {
    		$this->response->setContent($this->giveWarning('006'));	
    		return $this->response;
    	}

    	$photographer_unique =  str_ireplace('@','_',$photographer['email']);
    	$photographer_unique =  str_ireplace('.','_',$photographer_unique);


    	$k = $this->firebase->get('/photographers/'.$photographer_unique);
    	$tmo = json_decode($k, true);
    	if (sizeof($tmo) > 0) {
    		$this->response->setContent($this->giveWarning('007'));	
    		return $this->response;
    	}

        $photographer['password'] = md5($photographer['password']);
    	$this->firebase->set('/photographers/'.$photographer_unique, $photographer);
    	$this->response->setContent($this->giveWarning('002'));
		return $this->response;
    }

    public function updatePhotographerAction() {
    	$photographer = $this->request->getPost('photographer');
    	$photographer = json_decode($photographer, true);

    	if(!$photographer['email'] || $photographer['email'] == '') {
    		$this->response->setContent($this->giveWarning('006'));	
    		return $this->response;
    	}

    	$photographer_unique =  str_ireplace('@','_',$photographer['email']);
    	$photographer_unique =  str_ireplace('.','_',$photographer_unique);


    	$k = $this->firebase->get('/photographers/'.$photographer_unique);
    	$tmo = json_decode($k, true);
    	if (sizeof($tmo) == 0) {
    		$this->response->setContent($this->giveWarning('008'));	
    		return $this->response;
    	}

    	$this->firebase->update('/photographers/'.$photographer_unique, $photographer);
    	$this->response->setContent($this->giveWarning('002'));
		return $this->response;
    }

    /*
    *   normally will be used a key to determine the proeprty owner provider company. 
    * but again, this is a hackathon. fast fast fast!
    */
    public function pickAddressForPhotographer($photographer_unique, $address_unique, $key = '') {
        $uni = $this->firebase->get($this->default_path.'addresses/'.$address_unique);
        $this->firebase->set('/photographers/'.$photographer_unique.'/selectedAddresses/'.$address_unique, json_decode($uni,true));

        $this->response->setContent($this->giveWarning('002'));
        return $this->response;
    }


    /////////////////////////////////////////////////////////////
    /*
    * generic warnings from db
    */
    private function giveWarning($code) {
    	$warning = $this->firebase->get('/warning_codes/'.$code.'/');
    	return $warning;
    }

    public function warningAction($code) {
    	$this->response->setContent($this->giveWarning($code));
		return $this->response;
    }

    /*
    *	generic errors
    */
    private function giveError($c) {
    	if($c == '1') {
    		echo "contact with someone you like and cry1";
    		die();
    	} 

    	if($c == '2') {
    		echo "contact with someone you like and cry2";
    		die();
    	}
    }

}

