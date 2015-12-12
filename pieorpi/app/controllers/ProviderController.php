<?php

class ProviderController extends ControllerBase
{
    public function indexAction() {

    }

    public function signupAction() {

    }

    public function loginAction() {

    }

    public function dashAction() {
    	if(!$this->session->has('provider_unique')) {
    		echo "no!";
    		die();
    	}

    	$provider = $this->firebase->get('/providerUsers/'.$this->session->get('provider_unique'));
    	$provider = json_decode($provider, true);

    	$this->view->apikey = $provider['key'];
    	$this->view->mobilekey = $provider['key'];

    }

    public function logUserInAction() {
    	$email = $this->request->getPost('email');
    	$password = $this->request->getPost('password');
    	$password = md5($password);

    	if(!$email || $email == '') {
    		echo "problem";
    		die();
    	}

    	$provider_unique =  str_ireplace('@','_',$email);
    	$provider_unique =  str_ireplace('.','_',$provider_unique);

    	$provider = $this->firebase->get('/providerUsers/'.$provider_unique);
    	$provider = json_decode($provider, true);

    	if(sizeof($provider) == 0 ) {
    		echo "mno user";
    		die();
    	}

    	if($provider['password'] != $password) {
    		echo "password is wrong";
    		die();
    	}

    	$this->view->apikey = $provider['key'];
    	$this->view->mobilekey = $provider['key'];
    	$this->session->set('online', '1');
    	$this->session->set('provider_unique', $provider_unique);

    }

    public function createProviderAction() {
    	$email = $this->request->getPost('email');
    	$password = $this->request->getPost('password');


    	if(!$email || $email == '') {
    		echo "problem";
    		die();
    	}

    	$provider_unique =  str_ireplace('@','_',$email);
    	$provider_unique =  str_ireplace('.','_',$provider_unique);


    	$provider['email'] = $email;
    	$provider['password'] = md5($password);
    	$provider['key'] = $this->getToken(32);

    	$this->firebase->set('/providerUsers/'.$provider_unique, $provider );
    	$this->firebase->set('/providers/'.$provider['key'], array('createdAt' => time() ) );
    	$this->response->redirect('provider/login');
    }




    function crypto_rand_secure($min, $max) {
	        $range = $max - $min;
	        if ($range < 0) return $min; // not so random...
	        $log = log($range, 2);
	        $bytes = (int) ($log / 8) + 1; // length in bytes
	        $bits = (int) $log + 1; // length in bits
	        $filter = (int) (1 << $bits) - 1; // set all lower bits to 1
	        do {
	            $rnd = hexdec(bin2hex(openssl_random_pseudo_bytes($bytes)));
	            $rnd = $rnd & $filter; // discard irrelevant bits
	        } while ($rnd >= $range);
	        return $min + $rnd;
	}

	function getToken($length=32){
	    $token = "";
	    $codeAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	    $codeAlphabet.= "abcdefghijklmnopqrstuvwxyz";
	    $codeAlphabet.= "0123456789";
	    for($i=0;$i<$length;$i++){
	        $token .= $codeAlphabet[$this->crypto_rand_secure(0,strlen($codeAlphabet))];
	    }
	    return $token;
	}

	function logoutAction() {
		$this->session->destroy();
		$this->response->redirect('/');
	}
}

